//t=WScript.StdIn.ReadLine()
//s=WScript.StdIn.ReadLine()

s='anananaananas'
t='ananas'

m=t.length

alph=new Array()
for(i=0;i<m;i++)
	alph[t.charAt(i)]=0

del=new Array(m+1)
for(j=0;j<=m;j++)
	del[j]=new Array()

for(i in alph)
	del[0][i]=0
for(j=0;j<m;j++){
	prev=del[j][t.charAt(j)]
	del[j][t.charAt(j)]=j+1
	for(i in alph)
		del[j+1][i]=del[prev][i]
}

for(j=0;j<=m;j++){
	out=''
	out+=j+': '
	for(i in alph)
		out+=del[j][i]+' '
	WScript.Echo(out)
}
WSH.echo('------------------------------------')
k=0
q=del[0][s.charAt(0)]
res=new Array()
while(k<s.length)
{	
	WSH.echo(q,k,del[q][s.charAt(k)],s.charAt(k))
	q=del[q][s.charAt(k)]
	k++
	if(q==m)
		res.push(k-m+1)
}
WSH.echo('-------------------------------------')
WSH.echo(res)
