
function node(name,fr,used,ptr,code)
{
	this.name=name;
	this.fr=fr;
	this.used=used;
	this.ptr=ptr;
	this.code=code;
}
n1=new node('a',5,0,null,'');
WSH.echo(n1.used);
tree=new Array();
str = 'abracadabra';
alph=new Array();
for(i=0;i<str.length;i++)
alph[str.charAt(i)]=0
for(i=0;i<str.length;i++)
alph[str.charAt(i)]++
for(i in alph)
{
	n=new node(i,alph[i],0,null,'');
	tree.push(n);
}
WSH.echo(tree[1].name,' ',tree[1].fr);
for(i in tree)
{
	m1=tree[0].fr;
	m1_index=0;
	m2=tree[0].fr;//m1 and m2 не нужны оставить индексы
	m2_index=0;
	if (tree[i]<m1)
	{
		m2=m1;
		m2_index=m1_index;
		m1=tree[i];
		m1_index=i;
	}
	if (tree[i]<m2)
	{
		m2=tree[i];
		m2_index=i;
	}
}
tree.push(new node(tree[m1_index].name+tree[m2_index].name,tree[m1_index].fr+tree[m2_index].fr,0,null,''));