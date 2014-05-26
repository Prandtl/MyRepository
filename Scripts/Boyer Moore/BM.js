function GetBadSymbolTable(a){
	table=new Array()
	for (i=0;i<a.length;i++)
	{
		table[a.charAt(i)]=i+1;
	}
	return table
}

function GetSuffixOffset(suf,s){
	res=new Array()
	res.push(0)
	res.push(0)
	x=0
	i=0
	while(x!=-1)
	{
		x=s.indexOf(suf,i)
		res.push(x)
		i=x+suf.length
	}
	res.pop()
	res.pop()
	return res.pop()
}

function SuffixTable(a){
	table=new Array()
	k=1
	while(k<a.length)
	{
		suffix=a.slice(-k)
		table[suffix]=GetSuffixOffset(suffix,a)
		k++
	}
	for(i in table)
	{
		if (table[i]!=0)
			table[i]=a.length-table[i]-i.length+1
		else
			table[i]=a.length
	}
	
	table['']=1
	return table
}

function CheckEquality(s1,s2,SufT,BadT){
	m=s1.length-1
	WSH.echo(m)
	while(m>=0)
	{
		WSH.echo(s1.charAt(m),' ',s2.charAt(m))
		if(s1.charAt(m)==s2.charAt(m))
			m--
		else
		{
			if(BadT[s1.charAt(m)]==undefined)
				badc=s2.length
			else
				badc=s2.length-BadT[s1.charAt(m)]
			if(SufT[s1.slice(m+1)]==undefined||m+1==s1.length)
				sufoffset=1
			else
				sufoffset=SufT[s1.slice(m+1)]
			WSH.echo(badc,' ',sufoffset,' ',s1.slice(m+1))
			return Math.max(badc,sufoffset)
		}	
	}
	return -1
}

function BoyerMooreSearch(a,s){
	WSH.echo('Im here')
	res=new Array()
	badSym=GetBadSymbolTable(s)
	WSH.echo(badSym)
	suff=SuffixTable(s)
	for(i in suff)
	{
		WSH.echo(i)
		WSH.echo(suff[i])
		WSH.echo('------------------------')
	}
	for(i in badSym)
	{
		WSH.echo(i)
		WSH.echo(badSym[i])
		WSH.echo('------------------------')
	}
	WSH.echo('got tables')
	i=0
	while(i<a.length-s.length+1)
	{
		toCheck=a.slice(i,i+s.length)
		WSH.echo(toCheck)
		WSH.echo(s)
		z=CheckEquality(toCheck,s,suff,badSym)
		WSH.echo(z)
		if (z==-1)
		{
			res.push(i)
			i++
		}
		else
			i+=z;
		WSH.echo('now at ',i)
	}
	return res
}

WSH.echo(BoyerMooreSearch('abccabcbbccabcdabcdabc','abcdabc'))