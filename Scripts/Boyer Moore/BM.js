function GetBadSymbolTable(a){
	table=new Array()
	for (i=0;i<a.length;i++)
	{
		table[a.charAt(i)]=a.length+i-1;
	}
	return table
}

function PrefixFunc(a){
	l=a.length
	p=new Array(l)
	p[0]=0
	k=0
	for( i=1;i<l;i++)
	{
		while(k>0&&a.charAt(i)!=a.charAt(k))
		{
			k=p[k-1]
		}
		if(a.charAt(i)==a.charAt(k))
			k++
		p[i]=k
	}
	for(i in p)
		p[i]=parseFloat(p[i])
	return p
}

function StringReverse(a){
	s=''
	for (i=a.length-1;i>=0;i--)
	{
		s+=a.charAt(i)
	}
	return s
}

function SuffShift(a){
	m=a.length-1
	pi=PrefixFunc(a)
	pi1=PrefixFunc(StringReverse(a))
	suffshift=new Array()
	for (j=0;j<=m;j++)
	{
		suffshift[j]=a.length-pi[m]
		WSH.echo(suffshift[j])
	}
	for (i=1;i<=m;i++)
	{
		j=a.length-pi1[i]
		suffshift[j]=Math.min(suffshift[j],i-pi1[i])
		WSH.echo(suffshift[j])
	}
	return suffshift

}
WSH.echo(SuffShift('abcdadcd'))