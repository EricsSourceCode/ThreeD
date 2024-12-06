// Copyright Eric Chauvin 2018 - 2024.



// This is licensed under the GNU General
// Public License (GPL).  It is the
// same license that Linux has.
// https://www.gnu.org/licenses/gpl-3.0.html


// moveToDefaultView();



using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;




// namespace



class ThreeDScene
{
private MainData mData;
private PerspectiveCamera pCamera;
private Model3DGroup main3DGroup;
private ModelVisual3D mainModelVisual3D;
internal GeomModel geomModel;




private ThreeDScene()
{
}



internal ThreeDScene( MainData useMainData )
{
mData = useMainData;

try
{
pCamera = new PerspectiveCamera();
main3DGroup = new Model3DGroup();
mainModelVisual3D = new ModelVisual3D();
geomModel = new GeomModel( mData, main3DGroup );

setupCamera();
mainModelVisual3D.Content = main3DGroup;
moveToDefaultView();
}
catch( Exception ) // Except )
  {
  mData.showStatus(
      "Exception in ThreeScene constructor." );
  return;
  }
}



internal PerspectiveCamera getCamera()
{
return pCamera;
}




internal ModelVisual3D getMainModelVisual3D()
{
return mainModelVisual3D;
}



internal void setCameraTo( double X,
                           double Y,
                           double Z,
                           double LookX,
                           double LookY,
                           double LookZ,
                           double UpX,
                           double UpY,
                           double UpZ )
{
pCamera.Position = new Point3D( X, Y, Z );
pCamera.LookDirection = new Vector3D(
                         LookX, LookY, LookZ );
pCamera.UpDirection = new Vector3D(
                         UpX, UpY, UpZ );
}




internal void setCameraToOriginal()
{
pCamera.Position = new Point3D( 0, 0, 15 );
pCamera.LookDirection = new Vector3D( 0, 0, -1 );
pCamera.UpDirection = new Vector3D( 0, 1, 0 );
}




private void setupCamera()
{
// Positive Z values go toward the viewer.
pCamera.FieldOfView = 60;
// Clipping planes:
// Too much of a range for clipping will cause
// problems with the Depth buffer.

pCamera.FarPlaneDistance = 10000;
pCamera.NearPlaneDistance = 0.5;

setCameraToOriginal();
}


internal void moveForwardBack( double HowFar )
{
Vector3D lookAt = pCamera.LookDirection;
Point3D position = pCamera.Position;
Vector3D moveBy = new Vector3D();
moveBy = Vector3D.multiply( howFar, lookAt );
Point3D moveTo = new Point3D();
moveTo = Point3D.Add( position, moveBy );
PCamera.Position = moveTo;
}



internal void moveLeftRight( double angle )
{
Vector3D lookDirection = pCamera.LookDirection;
Vector3D upDirection = pCamera.UpDirection;

QuaternionEC.QuaternionRec axis;
axis.X = upDirection.X;
axis.Y = upDirection.Y;
axis.Z = upDirection.Z;
axis.W = 0;

QuaternionEC.QuaternionRec startPoint;
startPoint.X = lookDirection.X;
startPoint.Y = lookDirection.Y;
startPoint.Z = lookDirection.Z;
startPoint.W = 0;

QuaternionEC.QuaternionRec rotationQ =
          QuaternionEC.setAsRotation( axis,
                                      angle );

    QuaternionEC.QuaternionRec InverseRotationQ =
                    QuaternionEC.Inverse( RotationQ );

    QuaternionEC.QuaternionRec ResultPoint =
                  QuaternionEC.Rotate( RotationQ,
                                       InverseRotationQ,
                                       StartPoint );

    LookDirection.X = ResultPoint.X;
    LookDirection.Y = ResultPoint.Y;
    LookDirection.Z = ResultPoint.Z;
    PCamera.LookDirection = LookDirection;
    }



  // For Yaw, Pitch and Roll, this is Roll.
  internal void RotateLeftRight( double Angle )
    {
    Vector3D LookDirection = PCamera.LookDirection;
    Vector3D UpDirection = PCamera.UpDirection;

    QuaternionEC.QuaternionRec Axis;
    Axis.X = LookDirection.X;
    Axis.Y = LookDirection.Y;
    Axis.Z = LookDirection.Z;
    Axis.W = 0;

    Vector3.Vector Up;
    Up.X = UpDirection.X;
    Up.Y = UpDirection.Y;
    Up.Z = UpDirection.Z;

    QuaternionEC.QuaternionRec RotationQ =
                QuaternionEC.SetAsRotation( Axis,
                                            Angle );

    QuaternionEC.QuaternionRec InverseRotationQ =
                   QuaternionEC.Inverse( RotationQ );

    Vector3.Vector ResultPoint =
          QuaternionEC.RotateVector3( RotationQ,
                                      InverseRotationQ,
                                      Up );

    UpDirection.X = ResultPoint.X;
    UpDirection.Y = ResultPoint.Y;
    UpDirection.Z = ResultPoint.Z;
    PCamera.UpDirection = UpDirection;
    }



  internal void MoveUpDown( double Angle )
    {
    Vector3D LookDirection = PCamera.LookDirection;
    Vector3D UpDirection = PCamera.UpDirection;

    QuaternionEC.QuaternionRec Look;
    Look.X = LookDirection.X;
    Look.Y = LookDirection.Y;
    Look.Z = LookDirection.Z;
    Look.W = 0;

    QuaternionEC.QuaternionRec Up;
    Up.X = UpDirection.X;
    Up.Y = UpDirection.Y;
    Up.Z = UpDirection.Z;
    Up.W = 0;

    // X Cross Y = Z.  The Right-hand rule.

    QuaternionEC.QuaternionRec Cross =
                QuaternionEC.CrossProduct( Look, Up );

    QuaternionEC.QuaternionRec RotationQ =
           QuaternionEC.SetAsRotation( Cross, Angle );

    QuaternionEC.QuaternionRec InverseRotationQ =
                    QuaternionEC.Inverse( RotationQ );

    /////////////////
    // Rotate Up around Cross.
    QuaternionEC.QuaternionRec StartPoint;
    StartPoint.X = Up.X;
    StartPoint.Y = Up.Y;
    StartPoint.Z = Up.Z;
    StartPoint.W = 0;

    QuaternionEC.QuaternionRec ResultPoint =
              QuaternionEC.Rotate( RotationQ,
                                   InverseRotationQ,
                                   StartPoint );

    UpDirection.X = ResultPoint.X;
    UpDirection.Y = ResultPoint.Y;
    UpDirection.Z = ResultPoint.Z;
    PCamera.UpDirection = UpDirection;

    /////////////////
    // Rotate Look around Cross.
    StartPoint.X = Look.X;
    StartPoint.Y = Look.Y;
    StartPoint.Z = Look.Z;
    StartPoint.W = 0;

    ResultPoint = QuaternionEC.Rotate( RotationQ,
                                       InverseRotationQ,
                                       StartPoint );

    LookDirection.X = ResultPoint.X;
    LookDirection.Y = ResultPoint.Y;
    LookDirection.Z = ResultPoint.Z;
    PCamera.LookDirection = LookDirection;
    }



  internal void ShiftLeftRight( double HowFar )
    {
    Vector3D LookDirection = PCamera.LookDirection;
    Vector3D UpDirection = PCamera.UpDirection;

    QuaternionEC.QuaternionRec Look;
    Look.X = LookDirection.X;
    Look.Y = LookDirection.Y;
    Look.Z = LookDirection.Z;
    Look.W = 0;

    QuaternionEC.QuaternionRec Up;
    Up.X = UpDirection.X;
    Up.Y = UpDirection.Y;
    Up.Z = UpDirection.Z;
    Up.W = 0;

    QuaternionEC.QuaternionRec Cross =
                QuaternionEC.CrossProduct( Look, Up );

    Vector3D CrossVect = new Vector3D();
    CrossVect.X = Cross.X;
    CrossVect.Y = Cross.Y;
    CrossVect.Z = Cross.Z;

    Point3D Position = PCamera.Position;

    Vector3D MoveBy = Vector3D.Multiply( HowFar, CrossVect );
    Point3D MoveTo = Point3D.Add( Position, MoveBy );
    PCamera.Position = MoveTo;
    }



  internal void ShiftUpDown( double HowFar )
    {
    Vector3D UpDirection = PCamera.UpDirection;

    Point3D Position = PCamera.Position;
    Vector3D MoveBy = new Vector3D();
    Point3D MoveTo = new Point3D();

    MoveBy = Vector3D.Multiply( HowFar, UpDirection );
    MoveTo = Point3D.Add( Position, MoveBy );
    PCamera.Position = MoveTo;
    }



  internal void RotateView()
    {
    SolarS.RotateView();
    }



  internal void DoTimeStep()
    {
    SolarS.DoTimeStep();
    }
*/



internal void moveToDefaultView()
{
setCameraTo( 0, // X
             0, // Y
             0, // Z
             0,  // LookAt vector.
             1,
             0,
             0, // Up vector.
             0,
             1 ); // Up is with Z = 1.

}



/*
internal void SetEarthPositionToZero()
{
SolarS.SetEarthPositionToZero();
}

*/


} // Class
