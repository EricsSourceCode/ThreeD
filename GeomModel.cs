// Copyright Eric Chauvin 2018 - 2024.



// This is licensed under the GNU General
// Public License (GPL).  It is the
// same license that Linux has.
// https://www.gnu.org/licenses/gpl-3.0.html




// See the QuaternionEC.cs file for notes
// about the direction of rotation around
// an axis.




using System;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;


// namespace


class GeomModel
{
private MainData mData;

private Model3DGroup main3DGroup;
private SpaceObject[] spaceObjAr;
private int spaceObjLast = 0;
private const double scale = 300.0;
private Surface weights1;
//  private ECTime SunTime; // Local time.



private GeomModel()
{
}



internal GeomModel( MainData useMainData,
                    Model3DGroup use3DGroup )
{
mData = useMainData;

main3DGroup = use3DGroup;

//    SunTime = new ECTime();
//    SunTime.SetToNow();

spaceObjAr = new SpaceObject[2];
setupObjects();
}



private void setupObjects()
{
try
{
weights1 = new Surface( mData, "weights1" );
// Sun.TextureFileName = "C:\\Eric\\

addSpaceObject( weights1 );

makeNewGeometryModels();

}
catch( Exception ) // Except )
  {
  mData.showStatus( 
     "Exception in GeomModel.setupObjects()." );
  }
}



internal void addSpaceObject( 
                        SpaceObject toAdd )
{
====
spaceObjAr[spaceObjArrayLast] = ToAdd;
spaceObjLast++;

int length = spaceObjAr.Length;

if( spaceObjLast >= length )
  {
  Array.Resize( ref spaceObjAr, length + 16 );
  }
}




internal void makeNewGeometryModels()
{
main3DGroup.Children.Clear();

int last = spaceObjLast;
for( int count = 0; count < last; count++ )
  {
  spaceObjAr[count].makeNewGeometryModel();
  GeometryModel3D geoMod = SpaceObjectArray[
                    count].getGeometryModel();

  if( geoMod == null )
    continue;

  Main3DGroup.Children.Add( geoMod );
  }

setupAmbientLight();
setupMainLight();
}



/*
  internal void ResetGeometryModels()
    {
    Main3DGroup.Children.Clear();

    for( int Count = 0; Count < SpaceObjectArrayLast; Count++ )
      {
      GeometryModel3D GeoMod = SpaceObjectArray[Count].GetGeometryModel();
      if( GeoMod == null )
        continue;

      Main3DGroup.Children.Add( GeoMod );
      }

    setupAmbientLight();
    setupMainLight();
    }
*/



private void setupMainLight()
{
// Lights are Model3D objects.
// System.Windows.Media.Media3D.Model3D
// System.Windows.Media.Media3D.Light

setupPointLight( 0, // X
                 0, // Y
                 0 ); // Z

}



private void setupPointLight( double X,
                              double Y,
                              double Z )
{
PointLight pLight1 = new PointLight();
pLight1.Color = System.Windows.Media.
                               Colors.White;

Point3D location = new  Point3D( X, Y, Z );
pLight1.Position = location;
pLight1.Range = 100000000.0;

// Attenuation with distance D is like:
// Attenuation = C + L*D + Q*D^2

pLight1.ConstantAttenuation = 1;

    // PLight.LinearAttenuation = 1;
    // PLight.QuadraticAttenuation = 1;

main3DGroup.Children.Add( pLight1 );
}



private void setupAmbientLight()
{
byte RGB = 0x0F;
setupAmbientLightColors( RGB, RGB, RGB );
}



private void setupAmbientLightColors(
                                  byte red,
                                  byte green,
                                  byte blue )
{
try
{
AmbientLight ambiLight = new AmbientLight();
// AmbiLight.Color = System.Windows.
//         Media.Colors.Gray; // AliceBlue

Color ambiColor = new Color();
ambiColor.R = red;
ambiColor.G = green;
ambiColor.B = blue;

ambiLight.Color = ambiColor;

main3DGroup.Children.Add( ambiLight );
}
catch( Exception ) //  Except )
  {
  mData.showStatus(
    "Exception: ThreeDScene.setupAmbientLight()"
    );
  }
}



/*
  internal void RotateView()
    {
    // Sidereal period for one day on Earth is
    // 23 hours 56 minutes and 4.09053 seconds.
    double HoursInDay = 23.0d + (56.0d / 60.0d) +
                (4.09053d / (60.0d * 60.0d));

    double AddHours = NumbersEC.DegreesToRadians(
                   0.5 * (360.0d / HoursInDay) );
    Earth.UTCTimeRadians = Earth.UTCTimeRadians +
                                         AddHours;
    Earth.MakeNewGeometryModel();
    ResetGeometryModels();
    }




  internal void DoTimeStep()
    {
    const double TimeDelta = 60 * 10; // seconds.
    for( int Count = 0; Count < SpaceObjectArrayLast; Count++ )
      {
      SpaceObject SpaceObj = SpaceObjectArray[Count];
      SpaceObj.SetNextPositionFromVelocity(
                                    TimeDelta );
      }

    Vector3.Vector AccelVector = new Vector3.Vector();
    for( int Count = 0; Count < SpaceObjectArrayLast; Count++ )
      {
      SpaceObject SpaceObj = SpaceObjectArray[Count];
      SpaceObj.Acceleration = Vector3.MakeZero();

      for( int Count2 = 0; Count2 < SpaceObjectArrayLast; Count2++ )
        {
        SpaceObject FarAwaySpaceObj = SpaceObjectArray[Count2];
        if( FarAwaySpaceObj.Mass < 1 )
          throw( new Exception( "The space object has no mass." ));

        AccelVector = FarAwaySpaceObj.Position;
        AccelVector = Vector3.Subtract( AccelVector, SpaceObj.Position );

        double Distance = Vector3.Norm( AccelVector );

        // Check if it's the same planet at zero
        // distance.
        if( Distance < 1.0 )
          continue;

        double Acceleration =
             (ModelConstants.GravitationConstant *
             FarAwaySpaceObj.Mass) /
             (Distance * Distance);

        AccelVector = Vector3.Normalize( AccelVector );
        AccelVector = Vector3.MultiplyWithScalar( AccelVector, Acceleration );
        SpaceObj.Acceleration = Vector3.Add( SpaceObj.Acceleration, AccelVector );
        }

      // Add the new Acceleration vector to the
      // velocity vector.
      SpaceObj.Velocity = Vector3.Add( SpaceObj.Velocity, SpaceObj.Acceleration );
      }

    ShowStatus( " " );
    ShowStatus( "Velocity.X: " + Earth.Velocity.X.ToString( "N2" ));
    ShowStatus( "Velocity.Y: " + Earth.Velocity.Y.ToString( "N2" ));
    ShowStatus( "Velocity.Z: " + Earth.Velocity.Z.ToString( "N2" ));
    ShowStatus( " " );


    Earth.AddTimeStepRotateAngle();

    // Earth.SetPlanetGravityAcceleration( this );

    // Move Earth only:
    // Earth.MakeNewGeometryModel();
    // ResetGeometryModels();

    // Move all of the planets:
    MakeNewGeometryModels();
    }



  internal Vector3.Vector GetEarthScaledPosition()
    {
    Vector3.Vector ScaledPos;

    ScaledPos.X = Earth.Position.X * ModelConstants.ThreeDSizeScale;
    ScaledPos.Y = Earth.Position.Y * ModelConstants.ThreeDSizeScale;
    ScaledPos.Z = Earth.Position.Z * ModelConstants.ThreeDSizeScale;

    return ScaledPos;
    }



  internal void SetEarthPositionToZero()
    {
    Earth.Position.X = 0;
    Earth.Position.Y = 0;
    Earth.Position.Z = 0;

    // Make a new Earth geometry model before
    // calling this:
    // ResetGeometryModels();

    MakeNewGeometryModels();
    }

*/


} // Class
