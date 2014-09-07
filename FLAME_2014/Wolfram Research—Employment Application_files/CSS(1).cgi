/*************/
/* 2008 mods */
/*************/
img { behavior: url(/common/includes/iepngfix.htc); }

    body { margin: 0px; padding: 0px; background: #ffffff; }
    /* specific backgrounds for different sub-pages */
    body.Feedback { background: #ffffff url('http://www.wolfram.com/products/mathematica/images/mathematica-background.jpg') no-repeat; }
    body.Webresources { background: #ffffff url('/images/_webresources_back.jpg') no-repeat 0px 12px; }
    body.AppPacks { background: #ffffff url('http://www.wolfram.com/products/applications/images/background-appacks.jpg') no-repeat 0px 12px; }
    /* specific backgrounds for media.w.c */
    body.Media { background: #ffffff url('http://media.wolfram.com/images/media_background.jpg') no-repeat; }
    /* specific backgrounds for register.w.c */
    body.Register { background: #ffffff url('http://register.wolfram.com/common/images2003/background.jpg') no-repeat; }

    /* to replace deprecated tags */
    .nobr { white-space: nowrap; }
    .center { text-align: center; }

    /* styles needed by webforms update */
    .required { color: #FF0000; }
    .formError { color: #BB0000; }

    /* inline colors that come up a lot */
    .blue { color: #0000FF; }
    .midBlue { color: #336699; }
    .paleBlue { color: #E0E0FF; }
    .midpaleBlue { color: #7679D9; }
    .midpaleBlueWS { color: #6699CC; }
    .darkBlue { color: #272769; }
    .navy { color: #000080; }
    .black { color: #000000; }
    .white { color: #FFFFFF; }
    .red { color: #CC0000; }
    .wineRed { color: #993366; }
    .brightred { color: #FF0000; }
    .darkGrey { color: #606060; }
    .grey, .greyMid { color: #888888; } /* keeping greyMid temporarily just in case */
    .lightGrey { color: #CCCCCC; }
    .lightestGrey { color: #F0F0F0; }
    .orange { color: #FF6900; }
    .paleOrange { color: #FF944D; }
    .brightOrange { color: #FFCC33; }
    .purple { color: #662299; }
    .green { color: #008000; }

	/* forms that need 0 margin */
	form#emailbox { margin: 0; }

/*******************/
/* merge w/ M6.css */
/*******************/
body, p, td, tr, blockquote, smaller { font: 11px/15px Verdana,Geneva,Arial,sans-serif; }


a, a:link { color:#0e3eb9; text-decoration: none; outline: none; }
a:visited { color:#6080c2; text-decoration: none; outline: none; }
a:active, a:link:hover, a:visited:hover { text-decoration: none; color: #d86c00}

a.sblink { color: #0e3eb9; font-size: 10px; font-weight: bold; line-height: 12px; text-decoration: none; }
a.sblink:visited { color: #0e3eb9; }
a.sblink:hover { color: #d86c00;	}

.sblinkutil, .sblinkutil2, a.sblinkutil  { text-decoration: none; font-size: 10px; color: #4F6ECD; line-height: 10px; }
	a.sblinkutil:visited { color: #4F6ECD; }
	a.sblinkutil:hover 	{ color: #d86c00; }

a.sblinksub { text-decoration: none; font-size: 10px; color: #0E23CC; font-weight: bold; }
	a.sblinksub:visited { color: #6080c2; }
	a.sblinksub:hover { color: #d86c00; }
a.othermenu	{text-decoration: none; font-size: 10px; color: #ffffff; font-weight: bold}
	a.othermenu:visited { color: #ffffff;}
	a.othermenu:hover { color: #ffaa00;}
a.traillink, a.traillink:visited, a.traillink:hover { font: 10px/12px Verdana,Geneva,Arial,sans-serif; text-decoration: none; color: #d86c00; font-weight: bold; }

a.mathlink, a.mathlink:link, a.mathlink:visited {color: #cc0000; }
a.mathlink:hover { color: #DE6B00; }

a.blacklink:link, a.blacklink:visited { color: #000000; }
a.blacklink:hover, a.blacklink:active { color: #d86c00; }

a.chevron, .chevron { color: #ff9600; margin-left:3px; }
.doubleChevron { color:#bb0000; }

.trailarrow { text-decoration: none; font-size: 10px; color: #999999; }


.b { font-weight: bold; }
.i { font-style: italic; }


.backgroundTop { background: url(/common/images/2-1.gif) repeat-x top; }
.backgroundGreyLineLeft	{ background: url(/common/images/1-2background.gif) repeat-y}
.greytablebackground { background: url(/common/images/gradient-background.gif) repeat-x bottom; }


h1, h2, h3, h4, .inlineH2, .inlineH3 { font-family: Helvetica,Verdana,Arial,sans-serif; }
h1, h2 { padding: 0; margin: 0 0 20px 0; font-size: 20px; font-weight: normal; color: #3054af; line-height: 110%; }
h3 { color: #333333; font-size: 14px; font-weight: bold; padding-bottom: 0px; margin-bottom: 12px; line-height: 125%; }
h4 { color: #000000; font-size: 12px; font-weight: bold; margin-bottom: -0.6em; padding-bottom: 1em; }
.inlineH2 { font-size: 18px; font-weight: normal; line-height: 22px; }
	a.inlineH2:visited { color: #0e3eb9; }
	a.inlineH2:active { color: #d86c00; }
.inlineH3 { font-size: 14px; font-weight: bold; width: 450px; }
	a.inlineH3:visited { color: #6080c2; }
	a.inlineH3:active { color: #d86c00; }


.newsbox { font-weight: bold; color: #ff6600; }
.newsboxText { line-height: 12px; margin-bottom: 5px; }
.newsheader { font-weight: bold; color: #5e63b4; margin-top: 5px; margin-bottom: 4px; }


.floatingFramedBox { float: right; border: 1px solid #cccccc; padding: 4px; margin: 0px 0px 4px 10px; }
.nonfloatingFramedBox { margin-left: auto; margin-right: auto; padding: 0; width: 80%; border: 1px solid #cccccc; text-align: left; }
	.nonfloatingFramedBox .yellowtablebackground { margin: 0px; padding-bottom: 5px; }
	.nonfloatingFramedBox .yellowtablebackground .linkedbulletlist2 { margin-left: 10px; }


ul{ list-style-type: none; padding: 0; margin: 0; line-height:145%; }
ul li {background: url(/common/images/li-bullet1.gif) no-repeat 0 .18em; padding-left: 1.5em; margin: .65em 0; padding-bottom: 0; }
ul.linkedbulletlist1 li{ list-style-type: none; padding: 0; margin: 0; background: url(/common/images/orange-link-button.gif) no-repeat 0 .3em; padding-left: 1.2em; margin: 1em 0; line-height: 12px; }
	.linkedbulletlist1 { list-style-type: none; padding: 0; margin: 0; }
ul.linkedbulletlist2 li { list-style-type: none; padding: 0; margin: 0; background: url(/common/images/bullet_orange.gif) no-repeat 0 .5em; padding-left: 1em; margin: 1em 0; }
	.linkedbulletlist2 { list-style-type: none; padding: 0; margin: 0; }
	.yellowtablebackground	{ background: #ffffff url(/common/images/gradient-background-yellow.gif) repeat-x bottom; }
/*	
ul.sidebarList { text-align: left; margin: 0; padding: 0 8px 0 19px; border-right: 1px solid #cccccc; line-height: 12px; }
	ul.sidebarList li.lnk_bullet1 { margin: 0; padding: 2px 0 3px 1.5em; background: url('/common/images2003/lnk_bullet1.gif') no-repeat 0 2px; }
	ul.sidebarList li.lnk_arrow { margin: 0; padding: 2px 0 3px 1.5em; background: url('/common/images2003/lnk_arrow.gif') no-repeat 0 2px; }
ul.sidebarSublist { text-align: left; margin: 0; padding: 0; line-height: 12px; }
	ul.sidebarSublist li.lnk_bullet2 { margin: 0; padding: 4px 0 1px 1.35em; background: url('/common/images2003/lnk_bullet2.gif') no-repeat -1px 4px; }
	ul.sidebarSublist li.lnk_arrow { margin: 0; padding: 4px 0 1px 1.35em; background: url('/common/images2003/lnk_arrow.gif') no-repeat -1px 4px; }

.sidebarSeparator img { vertical-align: middle; }
*/

.quote { font: 12px/18px Georgia, "Times New Roman", Times, serif; font-style: italic; }
.quote2 { font: 13px16pt Georgia, "Times New Roman", Times, serif; font-style: italic; color: #3366cc; }
.bigquote { font: 16px/22px Times New Roman,Times,serif; font-style: italic; font-weight: bold; color: #d77411; letter-spacing: .08em; text-indent: 30px; }
.smallquote { font: 10px/14px Georgia, "Times New Roman", Times,serif; color: #666666; }


.highlight1 { font-style: oblique; font-weight: bold; line-height: 160%; color: #666666; }
.highlight2	{ font-style: oblique; font-weight: bold; line-height: 160%; color: #5E63B4; }


tt { font: 12px "Courier New", Courier, mono; font-weight: normal}
.largett { font: 18px/30px Courier New; color: #3054af; font-weight: normal; }
.smalltt		{ font: 11px "Courier New", Courier, mono; }


.leaded	{ line-height: 18pt; }
.small-leaded { font-size: 10px; line-height: 13pt; }
.smalltext		{ font-size: 10px; }
.tightersmalltext       { font-size: 10px; line-height: 12px}
.tighter { line-height: 12px; margin-top:3px; }


.caption { font-size: 10px; line-height: 14pt; color: #ffffff; }
.copyright { font-size: 9px; color: #999999; }
.date { text-decoration: none; font-weight: bold; }
.exampletitles { font-weight: bold; color: #ff9900; word-spacing: .08em; }


td.menu			{ font-size: 10px; font-weight: bold; cursor: pointer; cursor: hand; padding: 2px 5px 2px 5px; }
th			{ font-weight: bold; }
hr { border: none; border-top: 1px solid #ccc; border-bottom: 1px solid #efefef; width: 100%; height: 2px; margin: 10px auto; text-align: left; }
input { font: 10px Verdana,Geneva,Arial,sans-serif; color: #4f4f4f; font-weight: normal; padding-left:4px; }
code { font: 11px Courier New; font-weight: normal; }


.runninghead		{ text-decoration: none; font-size: 12px; color: #ffffff; font-weight: bold; }
.sbsection		{ text-decoration: none; font-weight: bold; }
.tableheader { font-size: 10px; color: #3D3D3D; letter-spacing: 0.04em; }
.topheader_site { padding: 16px 0px 0px 10px; margin-bottom:7px; background: #FFFFFF url(/common/images2003/headerBACK_blue.jpg); no-repeat; }
.topheader_M6 { background: #FFFFFF url(/common/images2003/headerBACK_M6.jpg) no-repeat; }


.searchboxsub { margin: 0px; padding: 1px 0px 0px 3px; font: 9px/14px Geneva, Helvetica, Arial, sans-serif; color: #999999; height: 14px; width: 120px; border: none; }
.searchboxsub-on { margin: 0px; padding: 0px 0px 0px 3px; font: 11px/14px Geneva, Helvetica, Arial, sans-serif; color: #000000; height: 14px; width: 120px; border: none; }
.searchboxborder { border: 1px solid #ccc; background-color: #fff; margin-left: 8px; margin-top: 0px; padding: 0px; }


div.mlinks { margin-top:15px; margin-left: 0px; }
	.mlinks .label{ color:gray; font-size:9px; }
	.mlinks table.outertable { width: 745px; border-top: solid 2px #cc0000; padding: 0px; margin:0px; }
		.mlinks table.outertable td { font-size: 10px; line-height: 1.7em; white-space: nowrap; }
table.iprow  { padding: 1px; margin: 0px; border:1px solid #cccccc; background: #ffffec; }
/*
table#oldfooter * { vertical-align: middle; }
*/

div.appAlertIE { text-align: center; }
  div.appAlert { width: 300px; margin: 30px auto; border: 1px solid #CCC; }
    div.appAlert div { background: #fff; border: solid #c00; border-width: 3px 0 1px; padding: 5px 10px; text-align: center; font-weight: bold; }
