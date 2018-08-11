public width, height, long;
public min,max;




new generateCollection(width, height, long){
  bool[,,] usageCollection = new bool[width][height][long];
  GameObject[,,] Collection = new GameObject[width][height][long];
  for(int i=0; i<width; i++){
    for(int j=0; j<height; j++){
      for(int k=0; k<long; k++){
        Collection[i][j][k]= generateCube;
      }
    }
  }
}

new hasNext(){
  for(int i=0; i<width; i++){
    for(int j=0; j<height; j++){
      for(int k=0; k<long; k++){
        if (usageCollection[i][j][k] != true)
        return true;
      }
    }
  }
  return false;
}

new getNext(){
  for(int i=0; i<width; i++){
    for(int j=0; j<height; j++){
      for(int k=0; k<long; k++){
        if (usageCollection[i][j][k] != true)
        return (i,j,k);
      }
    }
  }
  return false;
}

Shapes= tab[];




for (int i=0; hasNext(Collection); i++){
  Shapes[i]=generateShape(getNext(Collection));

}




generateShape(touple){
  GameObject Shape = new GameObject();

  x=touple(0);
  y=touple(1);
  z=touple(2);

  usageCollection[x,y,z]=true;
  Collection(x,y,z).transform.setParent(GameObject);

  int r=Rand(min,max);

  for(int i=0;i<r;i++){



    Vector3 cords=findNext(x,y,z) ?? null;

    if(!isNull(cords)) break

    x=cord[0];
    y=cord[1];
    z=cord[2];

    usageCollection[x,y,z]=true;
    Collection(x,y,z).transform.setParent(GameObject);
    }

}

findNext(int x, int y, int z,) {

  int Directions= new int[];

  if(usageCollection(x+1,y,z)&& x+1<=width){
    Directions.add(0);
  }
  if(usageCollection(x-1,y,z)&& x-1=>0){
    Directions.add(1);
  }
  if(usageCollection(x,y+1,z)&& y+1<=height){
    Directions.add(2);
  }
  if(usageCollection(x,y-1,z)&& y-1=>0){
    Directions.add(3);
  }
  if(usageCollection(x,y,z+1)&& z+1<=long){
    Directions.add(4);
  }
  if(usageCollection(x,y,z-1)&& z-1>=0){
    Directions.add(5);
  }

  if(Directions.size())==0)
    return null;

  int index = Rand(Directions.size())-1;


  switch(index){
  case 0:
  return x+1,y,z;

  case 1:
  return x-1,y,z;

  case 2:
  return x,y+1,z;

  case 3:
  return x,y-1,z;

  case 4:
  return x,y,z+1;

  case 5:
  return x,y,z-1;

  }
}
