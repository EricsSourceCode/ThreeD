// Copyright Eric Chauvin 2018 - 2024.



// This is licensed under the GNU General
// Public License (GPL).  It is the
// same license that Linux has.
// https://www.gnu.org/licenses/gpl-3.0.html



// This 3-D code comes from my old Climate
// Model project in the EarthOld repository.
// I did that in 2018.
// https://github.com/EricsSourceCode/EarthOld




using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Forms.Integration;



// namespace


// This is a generic 3D form to be used
// in any program.



public class ThreeDForm : Form
{
private MainData mData;
private System.Windows.Forms.Panel TopPanel;
private System.Windows.Forms.Panel ThreeDPanel;
private ElementHost MainElementHost;
private System.Windows.Forms.TextBox focusTextBox;
private Viewport3D viewPort;
private ThreeDScene scene;



private ThreeDForm()
{
}



internal ThreeDForm( MainData useMainData )
{
mData = useMainData;

try
{
setupGUI();

}
catch( Exception ) // Except )
  {
  mData.showStatus(
       "Exception in ThreeDForm constructor." );
  return;
  }
}



private void setupGUI()
{
TopPanel = new System.Windows.Forms.Panel();
ThreeDPanel = new System.Windows.Forms.Panel();
focusTextBox = new System.Windows.
                            Forms.TextBox();

ThreeDPanel.BackColor =
                   System.Drawing.Color.Black;

MainElementHost = new System.Windows.Forms.
                      Integration.ElementHost();
viewPort = new Viewport3D();

// ======
// Can it get keyboard input?
// viewPort.Focusable

initGuiComponents();

scene = mData.getScene();

MainElementHost.Child = viewPort;

viewPort.Children.Clear();
viewPort.Children.Add(
            scene.getMainModelVisual3D() );

viewPort.Camera = scene.getCamera();

// ========
// scene.visModel.getNewGeomModels();
}





private void initGuiComponents()
{
TopPanel.SuspendLayout();
ThreeDPanel.SuspendLayout();
this.SuspendLayout();

TopPanel.BorderStyle = System.Windows.
                Forms.BorderStyle.FixedSingle;
TopPanel.Controls.Add( focusTextBox );
TopPanel.Dock = System.Windows.Forms.
                                DockStyle.Top;
TopPanel.Location = new System.Drawing.
                                Point(0, 28);
TopPanel.Name = "TopPanel";
TopPanel.Size = new System.Drawing.
                               Size(617, 72);
TopPanel.TabIndex = 2;

ThreeDPanel.BorderStyle = System.Windows.
                Forms.BorderStyle.FixedSingle;
ThreeDPanel.Controls.Add(
                   this.MainElementHost );
ThreeDPanel.Dock = System.Windows.Forms.
                              DockStyle.Fill;
ThreeDPanel.Location = new System.
                         Drawing.Point(0, 109);
ThreeDPanel.Name = "ThreeDPanel";
ThreeDPanel.Size = new System.Drawing.
                              Size(617, 290);
ThreeDPanel.TabIndex = 3;

MainElementHost.Dock = System.Windows.
                         Forms.DockStyle.Fill;
MainElementHost.Location = new
                     System.Drawing.Point(0, 0);
MainElementHost.Name = "MainElementHost";
MainElementHost.Size = new System.
                        Drawing.Size(615, 301);
MainElementHost.TabIndex = 0;
MainElementHost.Text = "elementHost1";

focusTextBox.Location = new System.
                         Drawing.Point(26, 14);
focusTextBox.Name = "focusTextBox";
focusTextBox.Size = new System.Drawing.
                             Size( 50, 22 );
focusTextBox.TabIndex = 0;

this.AutoScaleMode = System.Windows.Forms.
                  AutoScaleMode.None;
this.ClientSize = new System.Drawing.
                              Size(617, 412);
this.Controls.Add( this.ThreeDPanel );
this.Controls.Add( this.TopPanel );
this.KeyPreview = true;
this.Name = "ThreeDForm";
this.Text = "ThreeDForm";
this.WindowState = System.Windows.Forms.
                    FormWindowState.Maximized;
this.KeyDown += new System.Windows.Forms.
                     KeyEventHandler(
                     this.ThreeDForm_KeyDown );
this.Font = new System.Drawing.Font(
             "Consolas", 34.0F,
             System.Drawing.FontStyle.Regular,
             System.Drawing.GraphicsUnit.Pixel,
             ((byte)(0)));

// this.FormClosing += new System.Windows.
//         Forms.FormClosingEventHandler(
//         this.MainForm_FormClosing );

TopPanel.ResumeLayout(false);
TopPanel.PerformLayout();
ThreeDPanel.ResumeLayout(false);
this.ResumeLayout(false);
this.PerformLayout();
}



internal void FreeEverything()
{
TopPanel.Dispose();
ThreeDPanel.Dispose();
MainElementHost.Dispose();
focusTextBox.Dispose();

// ViewPort.Dispose(); ??
// scene;
}




private void ThreeDForm_KeyDown(
                 object sender, KeyEventArgs e)
{
try
{
// A  The A key.
// Add The add key.
// D0 The 0 key.
// D1 The 1 key.
// Delete The DEL key.
// End The END key.
// Enter The ENTER key.
// Home The HOME key.
// Insert The INS key.
// NumPad2 The 2 key on the numeric keypad.
// Return The RETURN key.
// Space The SPACEBAR key.
// Subtract The subtract key.
// Tab The TAB key.

double angle = MathF.degreesToRadians( 2 );

if( e.Control )
  {
  if( e.KeyCode == Keys.D )
    {
    scene.moveToDefaultView();
    return;
    }

  if( e.KeyCode == Keys.Left )
    {
    scene.rotateLeftRight( -angle );
    return;
    }

  if( e.KeyCode == Keys.Right )
    {
    scene.rotateLeftRight( angle );
    return;
    }

  if( e.KeyCode == Keys.PageUp )
    {
    scene.moveForwardBack( 1000.0 );
    return;
    }

  if( e.KeyCode == Keys.PageDown )
    {
    scene.moveForwardBack( -1000.0 );
    return;
    }

  return;
  }


if( e.Alt )
  {
  return;
  }


if( e.Shift )
  {
  if( e.KeyCode == Keys.Left )
    {
    scene.shiftLeftRight( -3.0 );
    return;
    }

  if( e.KeyCode == Keys.Right )
    {
    scene.shiftLeftRight( 3.0 );
    return;
    }

  if( e.KeyCode == Keys.Up )
    {
    scene.shiftUpDown( 3.0 );
    return;
    }

  if( e.KeyCode == Keys.Down )
    {
    scene.shiftUpDown( -3.0 );
    return;
    }

  return;
  }

// if( e.KeyCode == Keys.Escape )

// if( e.KeyCode == Keys.F1 )

if( e.KeyCode == Keys.PageUp )
  {
  scene.moveForwardBack( 2.0 );
  }

if( e.KeyCode == Keys.PageDown )
  {
  scene.moveForwardBack( -2.0 );
  }

if( e.KeyCode == Keys.Left )
  {
  // See the notes in ThreeDScene.cs and
  // in Quatern.cs about rotation
  // directions and positive or negative
  // rotation angles.
  scene.moveLeftRight( angle );
  }

if( e.KeyCode == Keys.Right )
  {
  scene.moveLeftRight( -angle );
  }

if( e.KeyCode == Keys.Up )
  {
  scene.moveUpDown( angle );
  }

if( e.KeyCode == Keys.Down )
  {
  scene.moveUpDown( -angle );
  }
}
catch( Exception ) // Except )
  {
  mData.showStatus(
       "Exception in ThreeDForm.KeyDown." );
  }
}




} // Class
