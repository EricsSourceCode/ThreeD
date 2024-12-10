// Copyright Eric Chauvin 2018 - 2024.



// This is licensed under the GNU General
// Public License (GPL).  It is the
// same license that Linux has.
// https://www.gnu.org/licenses/gpl-3.0.html




// This is any object in 3D space.



using System;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;


// namespace


abstract class SpaceObject
{
protected MainData mData;
internal string objectName = "";
// internal Vector3.Vect position;

// internal Vector3.Vector Velocity;
// internal Vector3.Vector Acceleration;
// internal double Mass;



private SpaceObject()
{
}



internal SpaceObject( MainData useMainData,
                      string useName )
{
mData = useMainData;
objectName = useName;
}



abstract internal void makeNewGeomModel();

abstract internal GeometryModel3D
                         getGeometryModel();


// internal void SetNextPositionFromVelocity(



} // Class
