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

// Make any VisualModel class for the app.
internal VisualModel visModel;




private ThreeDScene()
{
}



internal ThreeDScene( MainData useMainData,
                      VisualModel useVisModel )
{
mData = useMainData;

pCamera = new PerspectiveCamera();
main3DGroup = new Model3DGroup();
mainModelVisual3D = new ModelVisual3D();
mainModelVisual3D.Content = main3DGroup;

visModel = useVisModel;
visModel.set3DGroup( main3DGroup );
visModel.setupObjects();

setupCamera();
moveToDefaultView();
}




internal PerspectiveCamera getCamera()
{
return pCamera;
}



internal Model3DGroup getMain3DGroup()
{
return main3DGroup;
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


internal void moveForwardBack( double howFar )
{
Vector3D lookAt = pCamera.LookDirection;
Point3D position = pCamera.Position;
Vector3D moveBy = new Vector3D();
moveBy = Vector3D.Multiply( howFar, lookAt );
Point3D moveTo = new Point3D();
moveTo = Point3D.Add( position, moveBy );
pCamera.Position = moveTo;
}



internal void moveLeftRight( double angle )
{
Vector3D lookDirection = pCamera.LookDirection;
Vector3D upDirection = pCamera.UpDirection;

Quatern.QuaternRec axis;
axis.X = upDirection.X;
axis.Y = upDirection.Y;
axis.Z = upDirection.Z;
axis.W = 0;

Quatern.QuaternRec startPoint;
startPoint.X = lookDirection.X;
startPoint.Y = lookDirection.Y;
startPoint.Z = lookDirection.Z;
startPoint.W = 0;

Quatern.QuaternRec rotationQ =
                 Quatern.setAsRotation(
                                axis, angle );

Quatern.QuaternRec inverseRotationQ =
                  Quatern.inverse( rotationQ );

Quatern.QuaternRec resultPoint =
             Quatern.rotate( rotationQ,
                             inverseRotationQ,
                             startPoint );

lookDirection.X = resultPoint.X;
lookDirection.Y = resultPoint.Y;
lookDirection.Z = resultPoint.Z;
pCamera.LookDirection = lookDirection;
}



// For Yaw, Pitch and Roll, this is Roll.

internal void rotateLeftRight( double angle )
{
Vector3D lookDirection = pCamera.LookDirection;
Vector3D upDirection = pCamera.UpDirection;

Quatern.QuaternRec axis;
axis.X = lookDirection.X;
axis.Y = lookDirection.Y;
axis.Z = lookDirection.Z;
axis.W = 0;

Vector3.Vect up;
up.X = upDirection.X;
up.Y = upDirection.Y;
up.Z = upDirection.Z;

Quatern.QuaternRec rotationQ =
                Quatern.setAsRotation(
                              axis, angle );

Quatern.QuaternRec inverseRotationQ =
                Quatern.inverse( rotationQ );

Vector3.Vect resultPoint =
                  Quatern.rotateVector3(
                             rotationQ,
                             inverseRotationQ,
                             up );

upDirection.X = resultPoint.X;
upDirection.Y = resultPoint.Y;
upDirection.Z = resultPoint.Z;
pCamera.UpDirection = upDirection;
}



internal void moveUpDown( double angle )
{
Vector3D lookDirection = pCamera.LookDirection;
Vector3D upDirection = pCamera.UpDirection;

Quatern.QuaternRec look;
look.X = lookDirection.X;
look.Y = lookDirection.Y;
look.Z = lookDirection.Z;
look.W = 0;

Quatern.QuaternRec up;
up.X = upDirection.X;
up.Y = upDirection.Y;
up.Z = upDirection.Z;
up.W = 0;

// X Cross Y = Z.  The Right-hand rule.

Quatern.QuaternRec cross =
           Quatern.crossProduct( look, up );

Quatern.QuaternRec rotationQ =
         Quatern.setAsRotation( cross, angle );

Quatern.QuaternRec inverseRotationQ =
                Quatern.inverse( rotationQ );

// Rotate Up around Cross.
Quatern.QuaternRec startPoint;
startPoint.X = up.X;
startPoint.Y = up.Y;
startPoint.Z = up.Z;
startPoint.W = 0;

Quatern.QuaternRec resultPoint =
            Quatern.rotate( rotationQ,
                            inverseRotationQ,
                            startPoint );

upDirection.X = resultPoint.X;
upDirection.Y = resultPoint.Y;
upDirection.Z = resultPoint.Z;
pCamera.UpDirection = upDirection;

// Rotate Look around Cross.
startPoint.X = look.X;
startPoint.Y = look.Y;
startPoint.Z = look.Z;
startPoint.W = 0;

resultPoint = Quatern.rotate(
                          rotationQ,
                          inverseRotationQ,
                          startPoint );

lookDirection.X = resultPoint.X;
lookDirection.Y = resultPoint.Y;
lookDirection.Z = resultPoint.Z;
pCamera.LookDirection = lookDirection;
}



internal void shiftLeftRight( double howFar )
{
Vector3D lookDirection = pCamera.LookDirection;
Vector3D upDirection = pCamera.UpDirection;

Quatern.QuaternRec look;
look.X = lookDirection.X;
look.Y = lookDirection.Y;
look.Z = lookDirection.Z;
look.W = 0;

Quatern.QuaternRec up;
up.X = upDirection.X;
up.Y = upDirection.Y;
up.Z = upDirection.Z;
up.W = 0;

Quatern.QuaternRec cross =
            Quatern.crossProduct( look, up );

Vector3D crossVect = new Vector3D();
crossVect.X = cross.X;
crossVect.Y = cross.Y;
crossVect.Z = cross.Z;

Point3D position = pCamera.Position;

Vector3D moveBy = Vector3D.Multiply(
                         howFar, crossVect );
Point3D moveTo = Point3D.Add( position, moveBy );
pCamera.Position = moveTo;
}



internal void shiftUpDown( double howFar )
{
Vector3D upDirection = pCamera.UpDirection;

Point3D position = pCamera.Position;
Vector3D moveBy = new Vector3D();
Point3D moveTo = new Point3D();

moveBy = Vector3D.Multiply( howFar,
                            upDirection );
moveTo = Point3D.Add( position, moveBy );
pCamera.Position = moveTo;
}




internal void moveToDefaultView()
{
// X is to the right, Y is up, Z is toward
// the camera.

setCameraTo( 0, // X
             0, // Y
             10, // Z
             0,  // LookAt vector.
             0,
             -1,
             0, // Up vector.
             1,
             0 );

}



} // Class
