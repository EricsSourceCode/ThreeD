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
// private SpaceObject[] SpaceObjectArray;
//  private int SpaceObjectArrayLast = 0;
//  private const double RadiusScale = 300.0;
//  private PlanetSphere Sun;
//  private ECTime SunTime; // Local time.



private GeomModel()
{
}



internal GeomModel( MainData useMainData,
                    Model3DGroup use3DGroup )
{
mData = useMainData;

main3DGroup = use3DGroup;

// The local time for the sun.
//    SunTime = new ECTime();
//    SunTime.SetToNow();

//     SpaceObjectArray = new SpaceObject[2];
//    AddInitialSpaceObjects();
}




/*
  internal void AddMinutesToSunTime( int Minutes )
    {
    SunTime.AddMinutes( Minutes );
    }



//////////
  private void InitializeTimes()
    {
    SunTime.SetToNow();

    // "Spring Equinox 2018 was at 10:15 AM on
    // March 20."
    SpringTime.SetUTCTime( 2018,
                            3,
                            20,
                            10,
                            15,
                            0,
                            0 );


    }
///////////





//////////
  private void SetPositionsAndTime( ECTime SetTime )
    {
    SunTime.Copy( SetTime );

    }
/////////




  private void AddInitialSpaceObjects()
    {
    try
    {
    // Sun:
    string JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLSun.txt";
    Sun = new PlanetSphere( MForm, "Sun", JPLFileName );
    Sun.Radius = 695700 * ModelConstants.TenTo3;
    Sun.Mass = ModelConstants.MassOfSun;
    Sun.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\sun.jpg";
    AddSpaceObject( Sun );

    // Earth:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLEarth.txt";
    Earth = new EarthGeoid( MForm, "Earth", JPLFileName );
    Earth.Mass = ModelConstants.MassOfEarth;
    Earth.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\earth.jpg";
    AddSpaceObject( Earth );

    // Moon:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLMoon.txt";
    Moon = new PlanetSphere( MForm, "Moon", JPLFileName );
    // Radius: About 1,737.1 kilometers.
    Moon.Radius = 1737100;
    Moon.Mass = ModelConstants.MassOfMoon;
    Moon.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\moon.jpg";
    AddSpaceObject( Moon );

    // Space Station:
    // Both Earth and the Space Station need to be
    // using the same time intervals for the JPL
    // data.
    // JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLSpaceStation.txt";
    // SpaceStation = new PlanetSphere( MForm, "Space Station", false, JPLFileName );
    // It's about 418 kilometers above the Earth.
    // SpaceStation.Radius = 400000; // 418000;
    // SpaceStation.Mass = 1.0;
    // SpaceStation.TextureFileName =
         // "C:\\Eric\\ClimateModel\\bin\\Release\\TestImage2.jpg";
    // AddSpaceObject( SpaceStation );




///////////
I would need to set this position after getting
Earth rotation angle and all that.
    // Leadville marker:
    JPLFileName = "";
    Leadville = new PlanetSphere( MForm, "Leadville", false, JPLFileName );
    Leadville.Radius = 1000000;
    Leadville.Mass = 0;
    Leadville.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\TestImage.jpg";
    AddSpaceObject( Leadville );
////////////



    // Mercury:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLMercury.txt";
    PlanetSphere Mercury = new PlanetSphere(
              MForm, "Mercury", JPLFileName );

    Mercury.Radius = 2440000d * RadiusScale;
    Mercury.Mass = ModelConstants.MassOfMercury;
    Mercury.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\Mercury.jpg";
    AddSpaceObject( Mercury );

    // Venus:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLVenus.txt";
    PlanetSphere Venus = new PlanetSphere(
                MForm, "Venus", JPLFileName );

    Venus.Radius = 6051000 * RadiusScale; // 6,051 km
    Venus.Mass = ModelConstants.MassOfVenus;
    Venus.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\Venus.jpg";
    AddSpaceObject( Venus );

    // Mars:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLMars.txt";
    PlanetSphere Mars = new PlanetSphere(
                 MForm, "Mars", JPLFileName );

    Mars.Radius = 3396000 * RadiusScale;
    Mars.Mass = ModelConstants.MassOfMars;
    Mars.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\mars.jpg";
    AddSpaceObject( Mars );


    // Jupiter:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLJupiter.txt";
    PlanetSphere Jupiter = new PlanetSphere(
              MForm, "Jupiter", JPLFileName );

    //                m  t
    Jupiter.Radius = 69911000d * RadiusScale; // 69,911 km
    Jupiter.Mass = ModelConstants.MassOfJupiter;
    Jupiter.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\Jupiter.jpg";
    AddSpaceObject( Jupiter );

    // Saturn:
    JPLFileName = "C:\\Eric\\ClimateModel\\EphemerisData\\JPLSaturn.txt";
    PlanetSphere Saturn = new PlanetSphere(
                MForm, "Saturn", JPLFileName );

    //               m  t
    Saturn.Radius = 58232000d * RadiusScale; // 58,232 km
    Saturn.Mass = ModelConstants.MassOfSaturn;
    Saturn.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\Saturn.jpg";
    AddSpaceObject( Saturn );

    // North Pole:
    PlanetSphere NorthPole = new PlanetSphere(
                MForm, "North Pole", "" );

    NorthPole.Radius = 400000d;
    NorthPole.Mass = 0;
    NorthPole.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\TestImage2.jpg";
    AddSpaceObject( NorthPole );

    // Moon Axis:
    PlanetSphere MoonAxis = new PlanetSphere(
                MForm, "Moon Axis", "" );

    MoonAxis.Radius = 300000d;
    MoonAxis.Mass = 0;
    MoonAxis.TextureFileName =
         "C:\\Eric\\ClimateModel\\bin\\Release\\moon.jpg";
    AddSpaceObject( MoonAxis );


    // Solar North:
    PlanetSphere SolarNorth = new PlanetSphere(
                MForm, "Solar North", "" );

    SolarNorth.Radius = 100000d;
    SolarNorth.Mass = 0;
    SolarNorth.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\TestImage2.jpg";
    AddSpaceObject( SolarNorth );

    // Sun Vector:
    SunSmall = new PlanetSphere(
                MForm, "Sun Small", "" );

    SunSmall.Radius = 500000d;
    SunSmall.Mass = 0;
    SunSmall.TextureFileName = "C:\\Eric\\ClimateModel\\bin\\Release\\Sun.jpg";
    AddSpaceObject( SunSmall );

    // Moon Vector:
    MoonSmall = new PlanetSphere(
                MForm, "Moon Small", "" );

    MoonSmall.Radius = 400000d;
    MoonSmall.Mass = 0;
    MoonSmall.TextureFileName =
    "C:\\Eric\\ClimateModel\\bin\\Release\\Moon.jpg";
    AddSpaceObject( MoonSmall );


    // Jupiter Vector:
    JupiterSmall = new PlanetSphere(
                MForm, "Jupiter Small", "" );

    JupiterSmall.Radius = 400000d;
    JupiterSmall.Mass = 0;
    JupiterSmall.TextureFileName =
    "C:\\Eric\\ClimateModel\\bin\\Release\\Jupiter.jpg";
    AddSpaceObject( JupiterSmall );


    // Mars Vector:
    MarsSmall = new PlanetSphere(
                MForm, "Mars Small", "" );

    MarsSmall.Radius = 300000d;
    MarsSmall.Mass = 0;
    MarsSmall.TextureFileName =
    "C:\\Eric\\ClimateModel\\bin\\Release\\Mars.jpg";
    AddSpaceObject( MarsSmall );


    // Venus Vector:
    VenusSmall = new PlanetSphere(
                MForm, "Venus Small", "" );

    VenusSmall.Radius = 300000d;
    VenusSmall.Mass = 0;
    VenusSmall.TextureFileName =
    "C:\\Eric\\ClimateModel\\bin\\" +
              "Release\\Venus.jpg";
    AddSpaceObject( VenusSmall );



    ///////////////////////////////////
    // Set the positions of the objects from the JPL data.
    SetJPLTimes();


    // Now that the Earth's position has been set...

    Vector3.Vector NorthPoleUnit = Earth.GetNorthPoleVector();

    Vector3.Vector NorthPolePos =
          Vector3.MultiplyWithScalar( NorthPoleUnit,
         ModelConstants.EarthRadiusMinor + NorthPole.Radius );

    NorthPolePos = Vector3.Add( NorthPolePos, Earth.Position );
    NorthPole.Position = NorthPolePos;


    // Set the moon axis.
    Vector3.Vector MoonFirstPosition = Moon.Position;
    // Subtract the Earth's position so this is just in relation
    // to the Earth.
    MoonFirstPosition = Vector3.Subtract( MoonFirstPosition, Earth.Position );

    ECTime OneWeek = new ECTime( SunTime.GetIndex());
    int SevenDays = 7 * 24 * 60;
    OneWeek.AddMinutes( SevenDays );

    JPLHorizonsData.JPLRec Rec =
           Moon.JPLData.GetNearestRecByDateTime( OneWeek.GetIndex());

    // if( Rec.CalDate == 0 ) then it's not found.

// Noticed that I've moved the positions of these vectors to
// a week later but I'm using them like
// they have their origins in the same place in order to get
// the cross product.

    Vector3.Vector MoonSecondPosition;
    MoonSecondPosition.X = Rec.PositionX;
    MoonSecondPosition.Y = Rec.PositionY;
    MoonSecondPosition.Z = Rec.PositionZ;

    // Get the Earth's position a week from now.
    Rec = Earth.JPLData.GetNearestRecByDateTime( OneWeek.GetIndex());

    Vector3.Vector EarthSecondPosition;
    EarthSecondPosition.X = Rec.PositionX;
    EarthSecondPosition.Y = Rec.PositionY;
    EarthSecondPosition.Z = Rec.PositionZ;


    MoonSecondPosition = Vector3.Subtract( MoonSecondPosition, EarthSecondPosition );

    // MoonFirstPosition = Vector3.Normalize( MoonFirstPosition );
    // MoonSecondPosition = Vector3.Normalize( MoonSecondPosition );

    Vector3.Vector MoonAxisPos = Vector3.CrossProduct(
                                           MoonFirstPosition,
                                           MoonSecondPosition );

    MoonAxisPos = Vector3.Normalize( MoonAxisPos );
    MoonAxisPos = Vector3.MultiplyWithScalar( MoonAxisPos,
              ModelConstants.EarthRadiusMinor + MoonAxis.Radius );

    MoonAxisPos = Vector3.Add( MoonAxisPos, Earth.Position );

    MoonAxis.Position = MoonAxisPos;


    Vector3.Vector SolarNorthPos;
    SolarNorthPos.X = 0;
    SolarNorthPos.Y = 0;
    SolarNorthPos.Z = 1;
    SolarNorthPos = Vector3.MultiplyWithScalar( SolarNorthPos,
              ModelConstants.EarthRadiusMinor + SolarNorth.Radius );

    SolarNorthPos = Vector3.Add( SolarNorthPos, Earth.Position );
    SolarNorth.Position = SolarNorthPos;


    // For SunSmall:
    Vector3.Vector SunVector = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    SunVector = Vector3.Negate( SunVector );

    // Add the vector from the center of the
    // coordinate system to the sun.
    SunVector = Vector3.Add( SunVector, Sun.Position );

    // This is now the vector from the Earth to the
    // sun.
    SunVector = Vector3.Normalize( SunVector );
    SunVector = Vector3.MultiplyWithScalar(
              SunVector,
              ModelConstants.EarthRadiusMajor + SunSmall.Radius );

    SunVector = Vector3.Add( SunVector,
                              Earth.Position );

    SunSmall.Position = SunVector;

    // For MoonSmall:
    Vector3.Vector MoonVector = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    MoonVector = Vector3.Negate( MoonVector );

    // Add the vector from the center of the
    // coordinate system to the Moon.
    MoonVector = Vector3.Add( MoonVector,
                              Moon.Position );

    // This is now the vector from the Earth to the
    // Moon.
    MoonVector = Vector3.Normalize( MoonVector );
    MoonVector = Vector3.MultiplyWithScalar(
              MoonVector,
              ModelConstants.EarthRadiusMajor +
              MoonSmall.Radius );

    MoonVector = Vector3.Add( MoonVector,
                              Earth.Position );

    MoonSmall.Position = MoonVector;

    // For JupiterSmall:
    Vector3.Vector JupiterVector = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    JupiterVector = Vector3.Negate( JupiterVector );

    JupiterVector = Vector3.Add( JupiterVector,
                              Jupiter.Position );

    // This is now the vector from the Earth to
    // Jupiter.
    JupiterVector = Vector3.Normalize( JupiterVector );
    JupiterVector = Vector3.MultiplyWithScalar(
              JupiterVector,
              ModelConstants.EarthRadiusMajor +
              JupiterSmall.Radius );

    JupiterVector = Vector3.Add( JupiterVector,
                              Earth.Position );

    JupiterSmall.Position = JupiterVector;

    // For MarsSmall:
    Vector3.Vector MarsVector = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    MarsVector = Vector3.Negate( MarsVector );

    // Add the vector from the center of the
    // coordinate system to Mars.
    MarsVector = Vector3.Add( MarsVector,
                              Mars.Position );

    // This is now the vector from the Earth to
    // Mars.
    MarsVector = Vector3.Normalize( MarsVector );
    MarsVector = Vector3.MultiplyWithScalar(
              MarsVector,
              ModelConstants.EarthRadiusMajor +
              MarsSmall.Radius );

    MarsVector = Vector3.Add( MarsVector,
                              Earth.Position );

    MarsSmall.Position = MarsVector;


    // For VenusSmall:
    Vector3.Vector VenusVector = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    VenusVector = Vector3.Negate( VenusVector );

    // Add the vector from the center of the
    // coordinate system to Venus.
    VenusVector = Vector3.Add( VenusVector,
                              Venus.Position );

    // This is now the vector from the Earth to
    // Venus.
    VenusVector = Vector3.Normalize( VenusVector );
    VenusVector = Vector3.MultiplyWithScalar(
              VenusVector,
              ModelConstants.EarthRadiusMajor +
              VenusSmall.Radius );

    VenusVector = Vector3.Add( VenusVector,
                              Earth.Position );

    VenusSmall.Position = VenusVector;

    }
    catch( Exception Except )
      {
      MForm.ShowStatus( "Exception in SolarSystem.AddMercury(): " + Except.Message );
      }
    }



  internal void SetJPLTimes()
    {
    SetToJPLTimePosition( SunTime.GetIndex() );

    SetEarthRotationAngle();

    MakeNewGeometryModels();
    }



  private void SetEarthRotationAngle()
    {
    // Coordinated Universal Time UTC
    // Time zero is midnight in Greenwich.
    // 12:00 is noon in Greenwich.

    // Earth: Sidereal period, hr  = 23.93419

    Vector3.Vector AlongX;
    AlongX.X = 1;
    AlongX.Y = 0;
    AlongX.Z = 0;

    Vector3.Vector AlongNegX;
    AlongNegX.X = -1;
    AlongNegX.Y = 0;
    AlongNegX.Z = 0;

    Vector3.Vector AlongY;
    AlongY.X = 0;
    AlongY.Y = 1;
    AlongY.Z = 0;

    Vector3.Vector AlongNegY;
    AlongNegY.X = 0;
    AlongNegY.Y = -1;
    AlongNegY.Z = 0;

    // The angle to the sun is noon Greenwich.

    Vector3.Vector EarthToSun = Earth.Position;

    // Make a vector that goes from the Earth to
    // the center of the coordinate system.
    EarthToSun = Vector3.Negate( EarthToSun );

    // Add the vector from the center of the
    // coordinate system to the sun.
    EarthToSun = Vector3.Add( EarthToSun, Sun.Position );

    // This is now the vector from the Earth to the
    // sun.  It's in the plane of the Earth/Sun.
    // The Barycentric coordinate system.


    ShowStatus( " " );
    ShowStatus( "EarthToSun.X: " + EarthToSun.X.ToString( "N2" ));
    ShowStatus( "EarthToSun.Y: " + EarthToSun.Y.ToString( "N2" ));
    ShowStatus( "EarthToSun.Z: " + EarthToSun.Z.ToString( "N2" ));

    EarthToSun = Vector3.Normalize( EarthToSun );

    // Pointing straight away from the sun for
    // midnight at zero time UTC.
    // Vector3.Vector EarthToSunNeg =
    //               Vector3.Negate( EarthToSun );

    // I need lat/long lines on the texture.

    double HalfPi = Math.PI / 2.0;
    double RotateBy = 0;
    Vector3.Vector NearAxis = AlongX;

    if( (EarthToSun.X < 0) &&
       (EarthToSun.Y > 0))
      {
      NearAxis = AlongY;
      RotateBy = HalfPi;
      }

    if( (EarthToSun.X < 0) &&
       (EarthToSun.Y < 0))
      {
      NearAxis = AlongNegX;
      RotateBy = HalfPi * 2;
      }

    if( (EarthToSun.X > 0) &&
       (EarthToSun.Y < 0))
      {
      NearAxis = AlongNegY;
      RotateBy = HalfPi * 3;
      }


    // The dot product of two normalized vectors.
    double Dot = Vector3.DotProduct(
                              NearAxis,
                              EarthToSun );

    double SunAngle = Math.Acos( Dot );

    SunAngle += RotateBy;


    ShowStatus( "Dot: " + Dot.ToString( "N2" ));
    ShowStatus( "SunAngle: " + SunAngle.ToString( "N2" ));
    ShowStatus( "HalfPi: " + HalfPi.ToString( "N2" ));


    // EarthToSun.X: -68,463,078,802.05
    // EarthToSun.Y: 135,732,403,641.45

    double Hours = SunTime.GetHour();
                   // SunTime.GetLocalHour();
    double Minutes = SunTime.GetMinute();
    Minutes = Minutes / 60.0d;
    Hours = Hours + Minutes;

    ShowStatus( "Greenwich hours: " + Hours.ToString( "N2" ));

    // Greenwich hours is 15.3 and here in Pine it
    // is 8:24 so Greenwich is 7 hours earlier.
    Hours = Hours + 12;
    ShowStatus( "Hours: " + Hours.ToString( "N2" ));

    // Sidereal period for one day on Earth is
    // 23 hours 56 minutes and 4.09053 seconds.
    // double HoursInDay = 23.0d + (56.0d / 60.0d) +
     //           (4.09053d / (60.0d * 60.0d));

    double HoursInDay = 24.0d;

    // 15 degrees per hour.
    double HoursInRadians = NumbersEC.
                   DegreesToRadians(
                   Hours * (360.0d / HoursInDay) );

    double Payson = NumbersEC.
                   DegreesToRadians(
                   -111.3392558 );

    // The sun angle is only within about a half
    // hour time because that's how I got the
    // JPL data.  A point each half hour.

    // If UTCTimeRadians is zero then Greenwhich
    // points along the positive X axis.
    // On my old Earth image Greenwich is in
    // about the center of the image.
    // The texture image has a Bounding Box of
    // vertexes at 180 longitude and -180 longitude.
    // So zero degrees is at the center.
    // If you are looking down on the Earth
    // from above the North pole, then a
    // positive number of Time Radians is a
    // counterclockwise rotations.  Just like
    // a regular x/y coordinate system.

    Earth.UTCTimeRadians = SunAngle +
                           HoursInRadians; // +
                           // -Payson;

    double HoursDiff = SunAngle - HoursInRadians;
    ShowStatus( "HoursDiff Radians: " + HoursDiff.
                                 ToString( "N2" ));

    // Make a new Earth geometry model before
    // calling reset.
    Earth.MakeNewGeometryModel();
    ResetGeometryModels();

    // MakeNewGeometryModels();
    }




  internal void AddSpaceObject( SpaceObject ToAdd )
    {
    SpaceObjectArray[SpaceObjectArrayLast] = ToAdd;
    SpaceObjectArrayLast++;
    if( SpaceObjectArrayLast >= SpaceObjectArray.Length )
      {
      Array.Resize( ref SpaceObjectArray, SpaceObjectArray.Length + 16 );
      }
    }



  private void ShowStatus( string ToShow )
    {
    if( MForm == null )
      return;

    MForm.ShowStatus( ToShow );
    }



  internal void SetToJPLTimePosition( ulong TimeIndex )
    {
    for( int Count = 0; Count < SpaceObjectArrayLast; Count++ )
      {
      SpaceObjectArray[Count].
                 SetToNearestJPLPosition( TimeIndex );

      }
    }
*/




internal void makeNewGeometryModels()
{
main3DGroup.Children.Clear();

/*
for( int Count = 0; Count <
          SpaceObjectArrayLast; Count++ )
  {
  SpaceObjectArray[Count].MakeNewGeometryModel();
  GeometryModel3D GeoMod = SpaceObjectArray[
                 Count].GetGeometryModel();
  if( GeoMod == null )
    continue;

  Main3DGroup.Children.Add( GeoMod );
  }
*/

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
