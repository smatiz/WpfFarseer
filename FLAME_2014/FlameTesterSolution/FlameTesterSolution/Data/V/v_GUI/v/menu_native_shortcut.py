from System.Windows.Forms import *

flame.ScripterControlForm.Menu = MainMenu()

#-------------------------------------------------------------------------------------------------------------

def add2(parent, text, action):
 menu_child = MenuItem()
 menu_child.Text = text
 menu_child.ShowShortcut = True
 menu_child.Click += action
 parent.MenuItems.Add(menu_child)
 return menu_child
 
def add(parent, text, shortcut, action):
 menu_child = add2(parent, text, action)
 menu_child.Shortcut =  shortcut
 
 

#-------------------------------------------------------------------------------------------------------------

menu_file = MenuItem()
menu_file.Text = "File"

add(menu_file, "New", Shortcut.CtrlN, lambda s,e : flame.ScripterControlForm.ScripterLoaderControl.NewDialog())
add(menu_file, "Open", Shortcut.CtrlO, lambda s,e : flame.ScripterControlForm.ScripterLoaderControl.LoadDialog())
add(menu_file, "Save", Shortcut.CtrlS, lambda s,e : flame.ScripterControlForm.ScripterLoaderControl.SaveDialog())
add(menu_file, "Save As", Shortcut.CtrlShiftS, lambda s,e : flame.ScripterControlForm.ScripterLoaderControl.SaveAsDialog())

#-------------------------------------------------------------------------------------------------------------


menu_edit = MenuItem()
menu_edit.Text = "Edit"

def keyBubble(o, kdb):
 if (not kdb.KeyEventArgs.Control and not kdb.KeyEventArgs.Shift):
  if (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.N):
   o.AddNewCellAfter(kdb.InOutControl)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.L):
   o.CopyCellFromPrevious(kdb.InOutControl)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.D):
   o.CopyCellToNext(kdb.InOutControl)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.R):
   o.RemoveCell(kdb.InOutControl);
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.Down):
   o.MoveDownCell(kdb.InOutControl);
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.Up):
   o.MoveUpCell(kdb.InOutControl)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.Alt and kdb.KeyEventArgs.KeyCode == Keys.F5):
   o.RefreshChilds()
   kdb.Handled = True
 if (kdb.KeyEventArgs.Control and kdb.KeyEventArgs.Shift and kdb.KeyEventArgs.Alt):
  if kdb.KeyEventArgs.KeyCode == Keys.P:
   changeLanguage(Flame.Dlr.Languages.Python)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.KeyCode == Keys.R):
   changeLanguage(Flame.Dlr.Languages.Ruby)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.KeyCode == Keys.S):
   changeLanguage(Flame.Dlr.Languages.Powershell)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.KeyCode == Keys.C):
   changeLanguage(Flame.Dlr.Languages.CSharp)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.KeyCode == Keys.J):
   changeLanguage(Flame.Dlr.Languages.Javascript)
   kdb.Handled = True
  elif (kdb.KeyEventArgs.KeyCode == Keys.A):
   toggleAutostart()
   kdb.Handled = True
   

add2(menu_edit, "New Cell\t\t(Alt + N)", lambda s,e : flame.ScripterControlForm.ScripterControl.NewCell())
add2(menu_edit, "Copy Cell From Previous\t\t(Alt + L)", lambda s,e : flame.ScripterControlForm.ScripterControl.CopyCellFromPrevious())
add2(menu_edit, "Copy Cell To Next\t\t(Alt + D)", lambda s,e : flame.ScripterControlForm.ScripterControl.CopyCellToNext())
add2(menu_edit, "Remove Cell\t\t(Alt + R)", lambda s,e : flame.ScripterControlForm.ScripterControl.RemoveCell())
add2(menu_edit, "Move Cell Up\t\t(Alt + Up)", lambda s,e : flame.ScripterControlForm.ScripterControl.MoveUpCell())
add2(menu_edit, "Move Cell Down\t\t(Alt + Down)", lambda s,e : flame.ScripterControlForm.ScripterControl.MoveDownCell())
add2(menu_edit, "Refresh Cells\t\t(Alt + F5)", lambda s,e : flame.ScripterControlForm.ScripterControl.RefreshChilds())

flame.ScripterControlForm.ScripterControl.KeyDownBubble += lambda s,e : keyBubble(s, e)


#-------------------------------------------------------------------------------------------------------------

menu_language = MenuItem()
menu_language.Text = "Language"

def changeLanguage(lang):
 flame.ScripterControlForm.ScripterControl.CurrentCell.Language = lang
 
add2(menu_language, "IronPython\t\t(Alt + Ctrl + Shift + P)", lambda s,e : changeLanguage(Flame.Dlr.Languages.Python))
add2(menu_language, "IronRuby\t\t(Alt + Ctrl + Shift + R)", lambda s,e : changeLanguage(Flame.Dlr.Languages.Ruby))
add2(menu_language, "C#\t\t(Alt + Ctrl + Shift + C)", lambda s,e : changeLanguage(Flame.Dlr.Languages.CSharp))
add2(menu_language, "Powershell\t\t(Alt + Ctrl + Shift + S)", lambda s,e : changeLanguage(Flame.Dlr.Languages.Powershell))

#-------------------------------------------------------------------------------------------------------------

menu_cell = MenuItem()
menu_cell.Text = "Cell"

def toggleAutostart():
 flame.ScripterControlForm.ScripterControl.CurrentCell.AutoStart = not flame.ScripterControlForm.ScripterControl.CurrentCell.AutoStart

def checkAutoStart():
 mmm.Checked = (flame.ScripterControlForm.ScripterControl.CurrentCell.AutoStart)

menu_cell.Select += lambda s,o : checkAutoStart()
mmm = add2(menu_cell, "start on load\t\t(Alt + Ctrl + Shift + A)", lambda s,e : toggleAutostart())



flame.ScripterControlForm.Menu.MenuItems.Add(menu_file)
menu_cell.MenuItems.Add(menu_edit)
menu_cell.MenuItems.Add(menu_language)
flame.ScripterControlForm.Menu.MenuItems.Add(menu_cell)