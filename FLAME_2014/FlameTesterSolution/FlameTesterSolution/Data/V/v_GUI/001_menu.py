from System import *
from System.Drawing import *
from System.Windows import *
from System.Windows.Forms import *

import System
import System.Drawing
import System.Windows.Forms
import Flame
import Flame.Dlr
import Flame.Controls.Common
import Flame.Controls
#import Flame.Reflection
#import ScintillaNET
import Flame.Scintilla
from Flame import *
#MessageBox.Show("aa")
flame.ScripterControlForm.Menu = MainMenu()


#-------------------------------------------------------------------------------------------------------------

def currentScripterLoaderControl():
 return flame.ScripterTabbedControl.ScripterLoaderControl
 
def currentScripterControl():
 return currentScripterLoaderControl().ScripterControl
 
def currentCell():
 return currentScripterControl().CurrentCell
 
#currentScripterControl().DefaultLanguage = Flame.Dlr.Languages.Python;


def mainMenu_addItem(parent, text, action):
 menu_child = MenuItem()
 menu_child.Text = text
 menu_child.ShowShortcut = True
 menu_child.Click += action
 parent.MenuItems.Add(menu_child)
 return menu_child

 

#-------------------------------------------------------------------------------------------------------------

menu_file = MenuItem()
menu_file.Text = "File"

mainMenu_addItem(menu_file, "New\t\t(Ctrl + N)",  lambda s,e : flame.ScripterTabbedControl.NewTab())
mainMenu_addItem(menu_file, "Open\t\t(Ctrl + O)",  lambda s,e : flame.ScripterTabbedControl.LoadTab())
mainMenu_addItem(menu_file, "Save\t\t(Ctrl + S)", lambda s,e : flame.ScripterTabbedControl.ScripterLoaderControl.SaveDialog())
mainMenu_addItem(menu_file, "Save As\t\t(Ctrl + Shift + S)", lambda s,e : flame.ScripterTabbedControl.ScripterLoaderControl.SaveAsDialog())
mainMenu_addItem(menu_file, "Close",  lambda s,e : flame.ScripterTabbedControl.CloseTab())
#mainMenu_addItem(menu_file, "Export to Txt", lambda s,e : flame.ScripterTabbedControl.ScripterLoaderControl.ExportTxtDialog())
#mainMenu_addItem(menu_file, "Import From Txt", lambda s,e : flame.ScripterTabbedControl.ImportTxtDialog())

#-------------------------------------------------------------------------------------------------------------



menu_edit = MenuItem()
menu_edit.Text = "Edit"




mainMenu_addItem(menu_edit, "New Cell\t\t(Alt + N)", lambda s,e : currentScripterControl().NewCell())
mainMenu_addItem(menu_edit, "Copy Cell From Previous\t\t(Alt + L)", lambda s,e : currentScripterControl().CopyCellFromPrevious())
mainMenu_addItem(menu_edit, "Copy Cell To Next\t\t(Alt + D)", lambda s,e : currentScripterControl().CopyCellToNext())
mainMenu_addItem(menu_edit, "Remove Cell\t\t(Alt + R)", lambda s,e : currentScripterControl().RemoveCell())
mainMenu_addItem(menu_edit, "Move Cell Up\t\t(Alt + Up)", lambda s,e : currentScripterControl().MoveUpCell())
mainMenu_addItem(menu_edit, "Move Cell Down\t\t(Alt + Down)", lambda s,e : currentScripterControl().MoveDownCell())
mainMenu_addItem(menu_edit, "Remove Output Cell", lambda s,e : currentScripterControl().RemoveOutputCell())
mainMenu_addItem(menu_edit, "Exec Cell\t\t(Shift + Enter)", lambda s,e : currentScripterControl().ExecCell())
mainMenu_addItem(menu_edit, "Refresh Cells\t\t(Alt + F5)", lambda s,e : currentScripterControl().RefreshChilds())


#-------------------------------------------------------------------------------------------------------------

menu_language = MenuItem()
menu_language.Text = "Language"

def mainMenu_changeLanguage(lang):
 c = currentCell()
 if c != None:
  c.Language = Flame.Dlr.Languages.Get(lang)
 
mainMenu_addItem(menu_language, "Text\t\t(Alt + Ctrl + Shift + T)", lambda s,e : mainMenu_changeLanguage("None"))
mainMenu_addItem(menu_language, "IronPython\t\t(Alt + Ctrl + Shift + P)", lambda s,e : mainMenu_changeLanguage("IronPythonExec"))
mainMenu_addItem(menu_language, "IronRuby\t\t(Alt + Ctrl + Shift + R)", lambda s,e : mainMenu_changeLanguage("IronRubyExec"))
mainMenu_addItem(menu_language, "C# Script\t\t(Alt + Ctrl + Shift + C)", lambda s,e : mainMenu_changeLanguage("CSharpScript"))
mainMenu_addItem(menu_language, "C#\t\t(Alt + Ctrl + Shift + K)", lambda s,e : mainMenu_changeLanguage("CSharp"))
mainMenu_addItem(menu_language, "Powershell\t\t(Alt + Ctrl + Shift + S)", lambda s,e : mainMenu_changeLanguage("Powershell"))
mainMenu_addItem(menu_language, "Javascript\t\t(Alt + Ctrl + Shift + J)", lambda s,e : mainMenu_changeLanguage("Javascript_ClearScript_Exex"))
#mainMenu_addItem(menu_language, "Unity\t\t(Alt + Ctrl + Shift + P)", lambda s,e : mainMenu_changeLanguage("HttpServerExec"))

#-------------------------------------------------------------------------------------------------------------

menu_cell = MenuItem()
menu_cell.Text = "Cell"

def mainMenu_toggleAutostart():
 c = currentCell()
 if c != None:
  c.AutoStart = not c.AutoStart

def mainMenu_checkAutoStart():
 c = currentCell()
 if c != None:
  mmm.Checked = (currentCell().AutoStart)
  
  
menu_cell.Select += lambda s,o : mainMenu_checkAutoStart()
mmm = mainMenu_addItem(menu_cell, "start on load\t\t(Alt + Ctrl + Shift + A)", lambda s,e : mainMenu_toggleAutostart())



flame.ScripterControlForm.Menu.MenuItems.Add(menu_file)
menu_cell.MenuItems.Add(menu_edit)
menu_cell.MenuItems.Add(menu_language)
flame.ScripterControlForm.Menu.MenuItems.Add(menu_cell)
