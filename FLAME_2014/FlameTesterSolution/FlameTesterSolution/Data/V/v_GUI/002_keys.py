
def mainMenu_mod(kdb, c, s, a):
 return c == kdb.KeyEventArgs.Control and s == kdb.KeyEventArgs.Shift and a == kdb.KeyEventArgs.Alt

def mainMenu_keyForm(o, kdb):
  MessageBox.Show("aa")
  if mainMenu_mod(kdb, True, False, False) and kdb.KeyEventArgs.KeyCode == Keys.N:
   flame.ScripterTabbedControl.NewTab()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, False, False) and kdb.KeyEventArgs.KeyCode == Keys.O:
   flame.ScripterTabbedControl.LoadTab()
   kdb.Handled = True
   
flame.ScripterControlForm.KeyDown += mainMenu_keyForm
flame.ScripterTabbedControl.KeyDown += mainMenu_keyForm


def mainMenu_keyBubble(o, kdb):
  if mainMenu_mod(kdb, True, False, False) and kdb.KeyEventArgs.KeyCode == Keys.N:
   flame.ScripterTabbedControl.NewTab()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, False, False) and kdb.KeyEventArgs.KeyCode == Keys.O:
   flame.ScripterTabbedControl.LoadTab()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, False, False) and kdb.KeyEventArgs.KeyCode == Keys.S:
   flame.ScripterTabbedControl.ScripterLoaderControl.SaveDialog()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, False) and kdb.KeyEventArgs.KeyCode == Keys.S:
   flame.ScripterTabbedControl.ScripterLoaderControl.SaveAsDialog()
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.N:
   o.AddNewCellAfter(kdb.InOutControl)
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.L:
   o.CopyCellFromPrevious(kdb.InOutControl)
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.D:
   o.CopyCellToNext(kdb.InOutControl)
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.R:
   o.RemoveCell(kdb.InOutControl);
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.Down:
   o.MoveDownCell(kdb.InOutControl);
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.Up:
   o.MoveUpCell(kdb.InOutControl)
   kdb.Handled = True
  elif mainMenu_mod(kdb, False, False, True) and kdb.KeyEventArgs.KeyCode == Keys.F5:
   o.RefreshChilds()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.T:
   mainMenu_changeLanguage("None")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.P:
   mainMenu_changeLanguage("IronPython")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.R:
   mainMenu_changeLanguage("IronRuby")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.S:
   mainMenu_changeLanguage("Powershell")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.K:
   mainMenu_changeLanguage("CSharp")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.C:
   mainMenu_changeLanguage("CSharpScript")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.J:
   mainMenu_changeLanguage("Javascript_ClearScript_Exex")
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.A:
   mainMenu_toggleAutostart()
   kdb.Handled = True
  elif mainMenu_mod(kdb, True, True, True) and kdb.KeyEventArgs.KeyCode == Keys.X:
   mainMenu_changeLanguage("HttpServerExec")
   kdb.Handled = True

def mainMenu_newtab_keys(s, e):
 e.ScripterLoaderControl.ScripterControl.KeyDownBubble += mainMenu_keyBubble

flame.ScripterTabbedControl.NewScripterControl += mainMenu_newtab_keys
