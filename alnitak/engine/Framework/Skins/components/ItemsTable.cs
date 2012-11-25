using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Alnitak.Exceptions;
using Language;

namespace Alnitak {

	/// <summary>
	/// Summary description for ItemsTable.
	/// </summary>
	public class ItemsTable : Control {

		#region Private Fields

		protected ItemsTableLine headerItem = null;

		protected ILanguageInfo info = CultureModule.getLanguage();

		protected string _tabletitleCssClass = string.Empty;
		protected string _tableCssClass = string.Empty;
		protected string _titleCssClass = string.Empty;
		protected string _title = string.Empty;

		protected int _index = -1;
		protected int _selectedIndex = -1;

		private static int _globalId = 0;

		private int _postId;

		#endregion

		#region Constructor

		public ItemsTable() {
			_postId = _globalId++;
		}

		#endregion

		#region Properties

		public ItemsTableLine HeaderItem {
			get{ return headerItem;}
			set{ headerItem = value; }
		}

		public string TitleCssClass {
			get{ return _titleCssClass;}
			set{ _titleCssClass = value; }
		}
		
		public string TableTitleCssClass {
			get{ return _tabletitleCssClass;}
			set{ _tabletitleCssClass = value; }
		}
		
		public string TableCssClass {
			get{ return _tableCssClass;}
			set{ _tableCssClass = value; }
		}

		public string Title {
			get{ return _title; }
			set{ _title = value; }
		}

		public int SelectedIndex {
			get{ return _selectedIndex; }
			set{ _selectedIndex = value; }
		}

		public int Count {
			get{ return Controls.Count; }
		}

		public int PostId {
			get{ return _postId; }
		}

		#endregion

		#region Public

		public void Reset() {
			_index = -1;
		}

		public void addLine( ItemsTableLine line ) {
			line.Index = ++_index;
			Controls.Add( line );
			foreach( ItemsTableItem item in line.Controls ) {
				item.Index = line.Index;
			}
		}

		public void removeLine( int index ) {
			removeLine( (ItemsTableLine)Controls[ index ] );
		}

		public void removeAllLines() {
			foreach( ItemsTableLine line in Controls ) {
				line.preRemove();
			}
			Controls.Clear();
		}

		public void removeLine( ItemsTableLine l ) {
			l.preRemove();
			Controls.Remove( l );
			int i = 0;
			foreach( ItemsTableLine line in Controls ) {
				if( line.Index != i ) {
					line.Index = i;
					foreach( ItemsTableItem item in line.Controls ) {
						item.Index = i;
					}
				}
				++i;
			}
		}

		public string getSpecificText( int index, int position ) {
			//try {
				return ((ItemsTableLine)Controls[index]).Controls[position].ToString();
			/*}catch( ArgumentOutOfRangeException  a ) {
				Exception e = new Chronos.Exceptions.ChronosException(string.Format("Count:{0},Index:{1};", Controls.Count, index));
				
			}*/
		}

		public Control getSpecificControl( int index, int position ) {
			return ((ItemsTableLine)Controls[index]).Controls[position];
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e) {
			_selectedIndex = -1;
			if( Page.IsPostBack ) {
				string value = Page.Request.Form[ID + "ItemsTableSelectedIndex"];
				if( value != null && value != string.Empty ) {
					SelectedIndex = int.Parse( value );
				}
			}

			base.OnInit (e);
		}

		protected override void Render(HtmlTextWriter writer) {
			writeTitle( writer );
			writer.WriteLine("<table class='" + TableCssClass + "'>");
			writeTableTitle( writer );
			writeItems( writer );
			writer.WriteLine("</table>");
			//base.Render (writer);
		}

		#endregion

		#region Rendering

		private void writeTitle( HtmlTextWriter writer ) {
			if( Title != string.Empty ) {
				writer.WriteLine("<div class='"+TitleCssClass+"' width='100%'>");
				writer.WriteLine("<b>"+ Title +"</b>");
				writer.WriteLine("</div>");
			}
		}
		
		/// <summary>Escreve o título da tabela</summary>
		private void writeTableTitle( HtmlTextWriter writer ) {
			if( null == headerItem )
				throw new AlnitakException("headerItem é null @ ItemsTable::writeTitle");

			writer.WriteLine( "<tr>" );
			foreach( ItemsTableItem title in HeaderItem.Controls ) {
				//writer.WriteLine("<td );
				title.RenderControl( writer );
				//writer.WriteLine("</b></td>");
			}
			writer.WriteLine( "</tr>" );

		}
		
		/// <summary>Escreve os items da tabela</summary>
		private void writeItems( HtmlTextWriter writer ) {
			foreach( ItemsTableLine item in Controls ) {
				writeItemsLine( writer, item );
			}
		}

		/// <summary>
		/// escreve uma linha de Items
		/// </summary>
		private void writeItemsLine( HtmlTextWriter writer, ItemsTableLine item ) {
			writer.WriteLine("<tr>");
			foreach( ItemsTableItem i in item.Controls ) {
				//writer.WriteLine( "<td class='resource' align='center' >" );
				i.RenderControl( writer );
				//writer.WriteLine( "</td>" );
			}

			writer.WriteLine("</tr>");
		}

		#endregion

		#region Private

		#endregion

	};

	#region Item Line

	/// <summary>
	/// representa uma linha de items
	/// </summary>
	public class ItemsTableLine : Control {
		
		private int _index;

		public int Index {
			get{ return _index; }
			set{ _index = value; }
		}

		public void add( ItemsTableItem item ) {
			Controls.Add( item );
		}

		public void preRemove() {
			foreach( ItemsTableItem i in Controls ) {
				i.preRemove();
			}
		}
			
		
	};

	#endregion

	#region Items

	/// <summary>
	/// item de uma tabela
	/// </summary>
	public abstract class ItemsTableItem : Control {
		
		protected string _item = string.Empty;
		protected string _cssclass = string.Empty;
		protected int _index;
		protected string _width = string.Empty;
		protected string _height = string.Empty;
		protected string _columnSpan = string.Empty;

		/// <summary>
		/// escreve o texto toWrite no writer segundo o formato correcto e comum
		/// </summary>
		protected void write( HtmlTextWriter writer, string toWrite ) {
			string w = _width==string.Empty?"":"width='" + _width + "'";
			string h = _height==string.Empty?"":"height='" + _height + "'";
			string colspan = _columnSpan ==string.Empty?"":"colspan='" + _columnSpan + "'";

			writer.WriteLine( "<td class='{0}' align='center' {1} {2} {3}>",_cssclass, w, h, colspan );
			writer.WriteLine( toWrite );
			writer.WriteLine( "</td>" );
		}

		public ItemsTableItem( string item ) {
			_item = item;
		}

		public string CssClass {
			get{ return _cssclass; }
			set{ _cssclass = value; }
		}

		public int ColumnSpan {
			get{ return int.Parse(_columnSpan); }
			set{ _columnSpan = value.ToString(); }
		}

		public string Item {
			get{ return _item; }
			set{ _item = value; }
		}

		public int Index {
			get{ return _index; }
			set{ _index = value; }
		}

		public override string ToString() {
			return _item;
		}

		public string Width {
			get{ return _width; }
			set{ _width = value; }
		}

		public string Height {
			get{ return _width; }
			set{ _width = value; }
		}

		public virtual void preRemove(){}
	};

	/// <summary>
	/// representa um item que é um texto
	/// </summary>
	public class ItemsTableText : ItemsTableItem {
		public ItemsTableText( string text, string css ) : base( text ) {
			CssClass = css;
		}

		protected override void Render(HtmlTextWriter writer) {
			if( CssClass != string.Empty )
				write( writer, string.Format("<span class='{0}'>{1}</span>",CssClass,Item) );
			else
				write( writer, Item );
		}

	}

	/// <summary>
	/// representa um item que é um link
	/// </summary>
	public class ItemsTableLink : ItemsTableItem {
		protected string _url = string.Empty;
		public ItemsTableLink( string text, string url, string css ) : base( text ) {
			_url = url;
			CssClass = css;
		}
		public string Url {
			get{ return _url; }
			set{ _url = value; }
		}
		
		protected override void Render(HtmlTextWriter writer) {
			if( CssClass != string.Empty )
				write( writer, string.Format("<a href='{0}' class='{1}' >{2}</a>",_url,CssClass,Item) );
			else
				write( writer, string.Format("<a href='{0}' >{1}</a>",_url,Item) );
		}
	}

	/// <summary>
	/// representa um item que é uma imagem
	/// </summary>
	public class ItemsTableImage : ItemsTableItem {
		
		public ItemsTableImage( string image ) : base( image ) {}
	
		protected override void Render(HtmlTextWriter writer) {
			string w = Width==string.Empty?"":"width='" + Width + "'";
			string h = Height==string.Empty?"":"height='" + Height + "'";


			if( CssClass != string.Empty )
				write( writer, string.Format("<img src='{0}' class='{1}' {2} {3} />",Item,CssClass,w,h) );
			else
				write( writer, string.Format("<img src='{0}' {1} {2} />",Item,w,h) );
		}
	}

	public class ItemsTableImageButton : ItemsTableItem {
		
		#region Fields

		protected ImageButton _imageButton = new ImageButton();
		private ArrayList handlers = new ArrayList();
		private string _id;

		#endregion

		#region Constructor

		public ItemsTableImageButton( string imageUrl, string id ): base( imageUrl ){
			_imageButton.ImageUrl = imageUrl;
			_id = id;
		}

		#endregion

		#region Events

		protected override void OnLoad(EventArgs e) {
			//_imageButton.Attributes.Add("OnClick",string.Format("submitItemsTableImageButton({0});",Index.ToString() ) );
			_imageButton.CausesValidation = false;
			_imageButton.CssClass = "imageButton";
			_imageButton.ID = _id;
			
			Controls.Add(_imageButton);
			base.OnLoad (e);
		}

		protected override void OnPreRender(EventArgs e) {
			//string id = "ItemsTable"+this.Parent.Parent.UniqueID.Replace(":","");
            registerScript();
			_imageButton.Attributes["OnClick"] = string.Format("submit"+Parent.Parent.ID+"ItemsTableImageButton({0});",Index.ToString() );
			base.OnPreRender (e);
		}


		protected override void Render(HtmlTextWriter writer) {
			string w = _width==string.Empty?"":"Width='" + Width + "'";
			writer.WriteLine( "<td class='resource' align='center' {0} >", w );

			base.Render (writer);
			
			writer.WriteLine( "</td>" );
		}

		#endregion

		#region Overrided

		public override void preRemove() {
			foreach( ImageClickEventHandler e in handlers ) {
				_imageButton.Click -= e;
			}
		}

		#endregion

		#region Public

		public void registerScript() {
			string script = @"
				<script language='javascript'>
					function submit"+ Parent.Parent.ID +@"ItemsTableImageButton( idx ) {
						var theform = document.pageContent;
						theform."+Parent.Parent.ID+@"ItemsTableSelectedIndex.value = idx;
					}
				</script>";
			Page.RegisterClientScriptBlock( Parent.Parent.ID+"ItemsTableImageButton",script);
			Page.RegisterHiddenField(Parent.Parent.ID+"ItemsTableSelectedIndex","");
		}

		public event ImageClickEventHandler Click {
			add {
				_imageButton.Click += value;
				handlers.Add( value );
			}
			remove {
				_imageButton.Click -= value;
				handlers.Remove( value );

			}
		}

		public override string ToString() {
			return Item;
		}

		#endregion
	}

	public class ItemsTableLinkButton : ItemsTableItem {
		
		#region Fields

		protected LinkButton _linkButton = new LinkButton();
		private ArrayList handlers = new ArrayList();
		
		#endregion

		#region Constructor

		public ItemsTableLinkButton( string text ): base( text) {
		}

		#endregion

		#region Events

		protected override void OnLoad(EventArgs e) {
			_linkButton.CausesValidation = false;
			_linkButton.Text = Item;
			
			Controls.Add(_linkButton);
			base.OnLoad (e);
		}

		protected override void OnPreRender(EventArgs e) {
			registerScript();
			_linkButton.Attributes["OnClick"] = string.Format("submit"+Parent.Parent.ID+"ItemsTableLinkButton({0});",Index.ToString() );
			base.OnPreRender (e);
		}

		#endregion

		#region Overrided

		protected override void Render(HtmlTextWriter writer) {
			writer.WriteLine( "<td class='resource' align='center' >");
			base.Render (writer);
			writer.WriteLine( "</td>" );
		}

		public override void preRemove() {
			foreach( EventHandler e in handlers ) {
				_linkButton.Click -= e;
			}
		}

		#endregion

		#region Public

		public void registerScript() {
			string script = @"
				<script language='javascript'>
					function submit"+ Parent.Parent.ID +@"ItemsTableLinkButton( idx ) {
						var theform = document.pageContent;
						theform."+Parent.Parent.ID+@"ItemsTableSelectedIndex.value = idx;
						alert(idx);
					}
				</script>";
			Page.RegisterClientScriptBlock( Parent.Parent.ID+"ItemsTableLinkButton",script);
			Page.RegisterHiddenField(Parent.Parent.ID+"ItemsTableSelectedIndex","");
		}

		public event EventHandler Click {
			add {
				_linkButton.Click += value;
				handlers.Add( value );
			}
			remove {
				_linkButton.Click -= value;
				handlers.Remove( value );

			}
		}

		public override string ToString() {
			return Item;
		}

		#endregion
	}

		/// <summary>
	/// representa um item que é uma imagem
	/// </summary>
	public class ItemsTableTextBox : ItemsTableItem {
		
		#region Fields

		protected TextBox _textBox = new TextBox();

		#endregion
		
		#region Constructor

		public ItemsTableTextBox(): base( "" ){
			_textBox.Text = "";
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e) {
			_textBox.CssClass = this.CssClass;
			Controls.Add(_textBox);
			base.OnInit (e);
		}

		protected override void Render(HtmlTextWriter writer) {
			string w = _width==string.Empty?"":"Width='" + Width + "'";
			writer.WriteLine( "<td class='resource' align='center' {0} >", w );
			
			base.Render (writer);
			
			writer.WriteLine( "</td>" );
		}

		#endregion

		#region Public

		public string Text {
			get { return _textBox.Text; }
			set { _textBox.Text = value; }
		}

		public override string ToString() {
			return _textBox.Text;
		}


		#endregion
	}
	
	#endregion
	
}
