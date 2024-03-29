if exists (select * from dbo.sysobjects where id = object_id(N'[Customer2163].[yaf_bitset]') and xtype in (N'FN', N'IF', N'TF'))
drop function [Customer2163].[yaf_bitset]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Customer2163].[yaf_forum_posts]') and xtype in (N'FN', N'IF', N'TF'))
drop function [Customer2163].[yaf_forum_posts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Customer2163].[yaf_forum_topics]') and xtype in (N'FN', N'IF', N'TF'))
drop function [Customer2163].[yaf_forum_topics]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[Customer2163].[yaf_vaccess]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [Customer2163].[yaf_vaccess]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


create view Customer2163.yaf_vaccess as
	select
		UserID				= a.UserID,
		ForumID				= x.ForumID,
		IsAdmin				= max(convert(int,b.Flags & 1)),
		IsGuest				= max(convert(int,b.Flags & 2)),
		IsForumModerator		= max(convert(int,b.Flags & 8)),
		IsModerator			= (select count(1) from dbo.yaf_UserGroup v,dbo.yaf_Group w,dbo.yaf_ForumAccess x,dbo.yaf_AccessMask y where v.UserID=a.UserID and w.GroupID=v.GroupID and x.GroupID=w.GroupID and y.AccessMaskID=x.AccessMaskID and (y.Flags & 64)<>0),
		ReadAccess			= max(x.ReadAccess),
		PostAccess			= max(x.PostAccess),
		ReplyAccess			= max(x.ReplyAccess),
		PriorityAccess			= max(x.PriorityAccess),
		PollAccess			= max(x.PollAccess),
		VoteAccess			= max(x.VoteAccess),
		ModeratorAccess			= max(x.ModeratorAccess),
		EditAccess			= max(x.EditAccess),
		DeleteAccess			= max(x.DeleteAccess),
		UploadAccess			= max(x.UploadAccess)
	from
		(select
			b.UserID,
			b.ForumID,
			ReadAccess		= convert(int,c.Flags & 1),
			PostAccess		= convert(int,c.Flags & 2),
			ReplyAccess		= convert(int,c.Flags & 4),
			PriorityAccess		= convert(int,c.Flags & 8),
			PollAccess		= convert(int,c.Flags & 16),
			VoteAccess		= convert(int,c.Flags & 32),
			ModeratorAccess		= convert(int,c.Flags & 64),
			EditAccess		= convert(int,c.Flags & 128),
			DeleteAccess		= convert(int,c.Flags & 256),
			UploadAccess		= convert(int,c.Flags & 512)
		from
			dbo.yaf_UserForum b
			join dbo.yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID
		
		union
		
		select
			b.UserID,
			c.ForumID,
			ReadAccess		= convert(int,d.Flags & 1),
			PostAccess		= convert(int,d.Flags & 2),
			ReplyAccess		= convert(int,d.Flags & 4),
			PriorityAccess		= convert(int,d.Flags & 8),
			PollAccess		= convert(int,d.Flags & 16),
			VoteAccess		= convert(int,d.Flags & 32),
			ModeratorAccess		= convert(int,d.Flags & 64),
			EditAccess		= convert(int,d.Flags & 128),
			DeleteAccess		= convert(int,d.Flags & 256),
			UploadAccess		= convert(int,d.Flags & 512)
		from
			dbo.yaf_UserGroup b
			join dbo.yaf_ForumAccess c on c.GroupID=b.GroupID
			join dbo.yaf_AccessMask d on d.AccessMaskID=c.AccessMaskID

		union

		select
			a.user_id,
			ForumID			= convert(int,0),
			ReadAccess		= convert(int,0),
			PostAccess		= convert(int,0),
			ReplyAccess		= convert(int,0),
			PriorityAccess	= convert(int,0),
			PollAccess		= convert(int,0),
			VoteAccess		= convert(int,0),
			ModeratorAccess	= convert(int,0),
			EditAccess		= convert(int,0),
			DeleteAccess	= convert(int,0),
			UploadAccess	= convert(int,0)
		from
			dbo.orionsbelt_users a
		) as x
		join dbo.yaf_UserGroup a on a.UserID=x.UserID
		join dbo.yaf_Group b on b.GroupID=a.GroupID
	group by a.UserID,x.ForumID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


create function yaf_bitset(@Flags int,@Mask int) returns bit as
begin
	declare @bool bit

	if (@Flags & @Mask) = @Mask
		set @bool = 1
	else
		set @bool = 0
		
	return @bool
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


create function yaf_forum_posts(@ForumID int) returns int as
begin
	declare @NumPosts int
	declare @tmp int

	select @NumPosts=NumPosts from Customer2163.yaf_Forum where ForumID=@ForumID

	if exists(select 1 from Customer2163.yaf_Forum where ParentID=@ForumID)
	begin
		declare c cursor for
		select ForumID from Customer2163.yaf_Forum
		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumPosts=@NumPosts+Customer2163.yaf_forum_posts(@tmp)
			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumPosts
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


create function yaf_forum_topics(@ForumID int) returns int as
begin
	declare @NumTopics int
	declare @tmp int

	select @NumTopics=NumTopics from Customer2163.yaf_Forum where ForumID=@ForumID

	if exists(select 1 from Customer2163.yaf_Forum where ParentID=@ForumID)
	begin
		declare c cursor for
		select ForumID from Customer2163.yaf_Forum
		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumTopics=@NumTopics+Customer2163.yaf_forum_topics(@tmp)
			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumTopics
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

