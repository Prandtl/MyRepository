function point(x,y)
{
	this.x=x;
	this.y=y;
	this.GetDistance= function(x,y)
	{
		return Math.sqrt((this.x-x)*(this.x-x)+(this.y-y)*(this.y-y));
	}
}
pointA=new point(0,0);
m=pointA.GetDistance(3,4);
WSH.echo(m);