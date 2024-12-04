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
focusTextBox = new System.Windows.Forms.TextBox();

ThreeDPanel.BackColor = Color.Black;

MainElementHost = new System.Windows.Forms.
                      Integration.ElementHost();
viewPort = new Viewport3D();

// ======
// Can it get keyboard input?
// viewPort.Focusable

initGuiComponents();

scene = new ThreeDScene( mData );
MainElementHost.Child = viewPort;

viewPort.Children.Clear();
viewPort.Children.Add(
            scene.getMainModelVisual3D() );

viewPort.Camera = scene.getCamera();
/*
scene.RefFrame.MakeNewGeometryModels();
// =======
// Scene.SolarS.MakeNewGeometryModels();
*/
}





private void initGuiComponents()
{
TopPanel.SuspendLayout();
ThreeDPanel.SuspendLayout();
this.SuspendLayout();

TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
// MainElementHost.Child = null;

focusTextBox.Location = new System.
                         Drawing.Point(26, 14);
focusTextBox.Name = "focusTextBox";
focusTextBox.Size = new System.Drawing.
                             Size( 50, 22 );
focusTextBox.TabIndex = 0;

// this.AutoScaleDimensions = new System.
//                      Drawing.SizeF(8F, 16F);
this.AutoScaleMode = System.Windows.Forms.
                  AutoScaleMode.None; // Font;
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
// Scene;
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

double Angle = MathF.DegreesToRadians( 2 );

/*
if( e.Control )
  {
  if( e.KeyCode == Keys.T )
    {
    Scene.RotateView();


  private void SetCameraTo( double X,
                            double Y,
                            double Z,
                            double LookX,
                            double LookY,
                            double LookZ,
                            double UpX,
                            double UpY,
                            double UpZ )

  return;
  }


if( e.KeyCode == Keys.S )
  {
  Scene.DoTimeStep();



////////////
  private void SetCameraTo( double X,
                            double Y,
                            double Z,
                            double LookX,
                            double LookY,
                            double LookZ,
                            double UpX,
                            double UpY,
                            double UpZ )
//////////////
        return;
        }

      if( e.KeyCode == Keys.E )
        {
        Scene.MoveToEarthView();
        return;
        }

      if( e.KeyCode == Keys.Z )
        {
        Scene.SetEarthPositionToZero();
        Scene.MoveToEarthView();
        return;
        }

      if( e.KeyCode == Keys.J )
        {
        Scene.SolarS.AddMinutesToSunTime( 10 );
        Scene.SolarS.SetJPLTimes();
        Scene.MoveToEarthView();
        return;
        }


      if( e.KeyCode == Keys.Left )
        {
        Scene.RotateLeftRight( -Angle );
        }

      if( e.KeyCode == Keys.Right )
        {
        Scene.RotateLeftRight( Angle );
        }

      if( e.KeyCode == Keys.PageUp )
        {
        Scene.MoveForwardBack( 1000.0 );
        }

      if( e.KeyCode == Keys.PageDown )
        {
        Scene.MoveForwardBack( -1000.0 );
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
        Scene.ShiftLeftRight( -3.0 );
        }

      if( e.KeyCode == Keys.Right )
        {
        Scene.ShiftLeftRight( 3.0 );
        }

      if( e.KeyCode == Keys.Up )
        {
        Scene.ShiftUpDown( 3.0 );
        }

      if( e.KeyCode == Keys.Down )
        {
        Scene.ShiftUpDown( -3.0 );
        }

      return;
      }

    if( e.KeyCode == Keys.Escape ) //  && (e.Alt || e.Control || e.Shift))
      {
      // MessageBox.Show( "Escape.", MainForm.MessageBoxTitle, MessageBoxButtons.OK );
      }

    if( e.KeyCode == Keys.F1 )
      {
      MessageBox.Show( "Control E gets back to Earth. Z goes to barycenter. J is plus 10 minutes.", MainForm.MessageBoxTitle, MessageBoxButtons.OK );
      return;
      }

    if( e.KeyCode == Keys.PageUp )
      {
      // MessageBox.Show( "Page up.", MainForm.MessageBoxTitle, MessageBoxButtons.OK );
      Scene.MoveForwardBack( 2.0 );
      }

    if( e.KeyCode == Keys.PageDown )
      {
      Scene.MoveForwardBack( -2.0 );
      }

    if( e.KeyCode == Keys.Left )
      {
      // See the notes in ThreeDScene.cs and in QuaternionEC.cs
      // about rotation directions and positive or negative
      // rotation angles.
      Scene.MoveLeftRight( Angle );
      }

    if( e.KeyCode == Keys.Right )
      {
      Scene.MoveLeftRight( -Angle );
      }

    if( e.KeyCode == Keys.Up )
      {
      Scene.MoveUpDown( Angle );
      }

    if( e.KeyCode == Keys.Down )
      {
      Scene.MoveUpDown( -Angle );
      }
*/

}
catch( Exception ) // Except )
  {
  mData.showStatus(
       "Exception in ThreeDForm.KeyDown." );
  }
}




} // Class
