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
	WSH.echo(i+' '+alph[i]);
	n=n+alph[i];
	e=e+1;
}
WSH.echo(n);
WSH.echo(e);
entropy=0;
q=Math.log(n)
if(e!=1)
{
	for(i in alph)
	{
		alph[i]=alph[i]/n;
		entropy=entropy+alph[i]*(Math.log(alph[i])/q);
	}
	entropy=-entropy;
}
WSH.echo('entropy is '+entropy);