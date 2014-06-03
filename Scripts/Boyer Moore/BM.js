s='abaxabaz'
t='aba'
n=s.length;
m=t.length;
bmBC=new Array()
for(j=0;j<m;j++)
	bmBC[t.charAt(j)]=j+1
i=0;
while(i!=n){
	j=m-1;
	l=0;
	while(s.charAt(i+j)==t.charAt(j) && t.charAt(j)!='')
	{
		l++;
		j--;
	}
	if(j<0)
	{
		WSH.Echo(i);
		i+=1;
	}
	else
	{
		i+=m-l- bmBC[t.charAt(j)]
	}
}