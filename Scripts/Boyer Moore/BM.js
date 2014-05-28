function PreBMbc(a){
	table=new Array()
	WSH.echo('BC starts')
	for (i=0;i<a.length;i++)
	{
		table[a.charAt(i)]=a.length-1-i;
		WSH.echo(a.charAt(i),table[a.charAt(i)])
	}
	
	WSH.echo('BC is done: ')
	for (i in table)
		WSH.echo(i,table[i])
	return table
}

function IsPrefix(x,p){
	j=0
	for(q=p;q<x.length;q++)
	{
		if(x.charAt(q)!=x.charAt(j))
			return false
	}
	return true
}

function SuffixLength(x,p){
	l=0
	i=p
	j=x.length-1
	while(i>=0&&x.charAt(i)==x.charAt(j))
	{
		l++
		i--
		j--
	}
	return l
} 	

function PreBMgs(x){
	WSH.echo('GS starts')
	
	m=x.length
	table=new Array()
	lastPrefixPosition=m
	for(i=m-1;i>=0;i-=1)
	{	
		if(IsPrefix(x,i+1))
		{
			lastPrefixPosition=i+1
		}
		table[m-1-i]=lastPrefixPosition-i+m-1
	}
	for(i=0;i<m-1;i++)
	{
		slen=SuffixLength(x,i)
		table[slen]=m-1-i+slen
	}
	WSH.echo('GS is done:')
	for (i in table)
		WSH.echo(i,table[i])
	return table
}	

function BM(y,x){
	res= new Array()
	
	n=y.length
	m=x.length
	if(m==0)
		return res
	
	WSH.echo('preBM starts')
	bmBC=PreBMbc(x)
	bmGS=PreBMgs(x)
	WSH.echo('preBM ended successfully')
	WSH.echo()
	
	for(i=m-1;i<n;i++)
	{
		j=m-1
		while(x.charAt(j)==x.charAt(i)&&(i>=0&&j>=0))
		{
			WSH.echo('i: ',i,'; ',x.charAt(i))
			WSH.echo('j: ',j,'; ',x.charAt(j))
			WSH.echo('-------------------------')
			if (j==0)
				res.push(i)
			i--
			j--
		}
		if (bmBC[y.charAt(i)]==undefined)
			bcShift=m
		else
			bcShift=bmBC[y.charAt(i)]
		i+=Math.max(bmGS[m-1-j],bcShift)
	}
	
	return res
}

WSH.echo(BM('abeccaabadbabbad','abbad'))