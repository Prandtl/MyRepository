str=WScript.StdIn.ReadLine();
alph=new Array();
for(i=0;i<str.length;i++)
	alph[str.charAt(i)]=0;
for(i=0;i<str.length;i++)
	alph[str.charAt(i)]++;
WSH.echo(alph.length);
n=0;
e=0;
for(i in alph)
{
	WSH.echo(alph[i]);
	e++;
}
WSH.echo(e);
entropy=0;
if(e>1)
{
	for(i in alph)
	{
		fr=alph[i]/str.length;
		entropy+=fr*(Math.log(fr));
	}
	WSH.echo(entropy);
	entropy=entropy/Math.log(e);
}
WSH.echo('entropy is '+entropy*(-1));