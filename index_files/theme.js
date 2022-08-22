
//var cmDefaultBase = '/JSCookMenu/ThemeIE/';

// the follow block allows user to re-define theme base directory
// before it is loaded.
try
{
	if (myDefaultBase)
	{
		cmDefaultBase = myDefaultBase;
	}
}
catch (e)
{
}

var cmDefault =
{
  	// main menu display attributes
  	//
  	// Note.  When the menu bar is horizontal,
  	// mainFolderLeft and mainFolderRight are
  	// put in <span></span>.  When the menu
  	// bar is vertical, they would be put in
  	// a separate TD cell.

  	// HTML code to the left of the folder item
  	mainFolderLeft: '',
  	// HTML code to the right of the folder item
  	mainFolderRight: '',
	// HTML code to the left of the regular item
	mainItemLeft: '',
	// HTML code to the right of the regular item
	mainItemRight: '',

	// sub menu display attributes

	// HTML code to the left of the folder item
	//folderLeft: '<img alt="" src="../../../../_templates/_system/menuThemes/default/' + cmDefaultBase + 'folder.gif">',
	folderLeft:'',
	// HTML code to the right of the folder item
	//folderRight: '<img alt="" src="../../../../_templates/_system/menuThemes/default/' + cmDefaultBase + 'arrow.gif">',
	folderRight:'',
	// HTML code to the left of the regular item
	//itemLeft: '<img alt="" src="../../../../_templates/_system/menuThemes/default/' + cmDefaultBase + 'link.gif">',
	itemLeft:'',
	// HTML code to the right of the regular item
	itemRight: '',
	// cell spacing for main menu
	mainSpacing: 0,
	// cell spacing for sub menus
	subSpacing: 0,
	// auto dispear time for submenus in milli-seconds
	delay: 100
};

// horizontal split, used only in sub menus
var cmDefaultHSplit = [_cmNoClick, '<td colspan="3" style="height: 3px; overflow: hidden"><div class="cmDefaultMenuSplit"></div></td>'];
// vertical split, used only in main menu
var cmDefaultMainVSplit = [_cmNoClick, '<div class="cmDefaultMenuVSplit"></div>'];
// horizontal split, used only in main menu
var cmDefaultMainHSplit = [_cmNoClick, '<td colspan="3"><div class="cmDefaultMenuSplit"></div></td>'];
