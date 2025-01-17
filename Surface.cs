// Copyright Eric Chauvin 2018 - 2025.



// This is licensed under the GNU General
// Public License (GPL).  It is the
// same license that Linux has.
// https://www.gnu.org/licenses/gpl-3.0.html


using System;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;



// namespace




class Surface
{
// internal string textureFileName = "";
private MeshGeometry3D mesh;
private GeometryModel3D geomMod;



internal Surface( MainData useMainData )
{
geomMod = new GeometryModel3D();
mesh = new MeshGeometry3D();
geomMod.Geometry = mesh;
}



internal void clear()
{
mesh.Positions.Clear();
mesh.Normals.Clear();
mesh.TriangleIndices.Clear();
mesh.TextureCoordinates.Clear();
}


internal void clearNormals()
{
mesh.Normals.Clear();
}


internal GeometryModel3D getGeometryModel()
{
return geomMod;
}



internal void addVertex( double x,
                        double y,
                        double z )
{
// If TriangleIndices are not specified then
// every three vertex points become a
// triangle.

Point3D vertex = new Point3D( x, y, z );

// mesh.Positions.Count
// mesh.Positions.Items[Index];

mesh.Positions.Add( vertex );
}



internal void addNormal( double x,
                        double y,
                        double z )
{
Vector3D norm = new Vector3D( x, y, z );
mesh.Normals.Add( norm );
}




internal void addTexturePt( double x,
                            double y )
{
// Texture coordinates are "scaled by their
// bounding box".  You have to create the right
// "bounding box."  You have to give it bounds
// by setting vertexes out on the edges.
// For example with latitude/longitude,
// you have to set both the North Pole and
// the South Pole vertexes in order to give
// the north and south latitudes a "bounding box"
// so that the texture can be scaled all the way
// from north to south.  And you have to set
// vertexes at 180 longitude and -180 longitude
// (out on the edges) to give it the right
// bounding box for longitude.  Otherwise it
// will scale the texture image in ways you
// don't want.

Point texPoint = new Point( x, y );
mesh.TextureCoordinates.Add( texPoint );
}




internal void addTriangleIndex( int index1,
                               int index2,
                               int index3 )
{
mesh.TriangleIndices.Add( index1 );
mesh.TriangleIndices.Add( index2 );
mesh.TriangleIndices.Add( index3 );
}



internal void setMaterialBlue()
{
DiffuseMaterial solidMat = new DiffuseMaterial();

solidMat.Brush = Brushes.Blue;
// solidMat.Brush = setTextureImageBrush();

// There is only one material for this
// GeometryModel3D.

geomMod.Material = solidMat;
}



} // Class
