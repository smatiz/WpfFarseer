from System.IO import *


def getImage(s):
 return System.Drawing.Image.FromFile(System.IO.FileInfo(Flame.Dlr.PathsHelper.MainPath + "\\GUI\\" + s).FullName)

def x(s, e):
 p = System.Windows.Forms.Panel()
 if e.TextInput.Language.Name == "PowershellExec":
  e.InfoPanel.BackColor = System.Drawing.Color.LightGray
  p.BackgroundImage = getImage("icon_powershell.png")
 elif e.TextInput.Language.Name  == "IronPythonExec":
  e.InfoPanel.BackColor = System.Drawing.Color.LightGreen
  p.BackgroundImage = getImage("icon_python.png")
 elif e.TextInput.Language.Name == "IronRubyExec":
  e.InfoPanel.BackColor = System.Drawing.Color.Salmon
  p.BackgroundImage = getImage("icon_ruby.png")
 elif e.TextInput.Language.Name == "CSharpCompiler":
  e.InfoPanel.BackColor = System.Drawing.Color.Blue
  p.BackgroundImage = getImage("icon_csharp.png")
 elif e.TextInput.Language.Name == "CSharpExec":
  e.InfoPanel.BackColor = System.Drawing.Color.LightBlue
  p.BackgroundImage = getImage("icon_csharp.png")
 elif e.TextInput.Language.Name == "Javascript_ClearScript_Exex":
  e.InfoPanel.BackColor = System.Drawing.Color.Violet
  p.BackgroundImage = getImage("icon_js.png")
 else:
  e.InfoPanel.BackColor = System.Drawing.Color.Black
  p.BackgroundImage = getImage("icon_txt.png")
 p.Width = 15
 p.Top = 2
 p.Left = 2
 p.Height = p.Width
 p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
 e.InfoPanel.Controls.Clear()
 e.InfoPanel.Controls.Add(p)

flame.ScripterTabbedControl.TextInputPropertyChanged += x
 
#def mainMenu_newtab_infoPanel(s, e):
# flame.ScripterTabbedControl.TextInputPropertyChanged += x
 #e.ScripterLoaderControl.ScripterControl.KeyDownBubble += mainMenu_keyBubble

#flame.ScripterTabbedControl.NewScripterControl += mainMenu_newtab_infoPanel

#MessageBox.Show("aa")
