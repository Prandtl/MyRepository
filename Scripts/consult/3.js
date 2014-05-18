k=2.54
function inToCm($0,$1) {
	n=parseFloat($1)*k
	s=n.toString()
	return n.toString() + " cm";
}
var s = 'something-something 12 in 13.2 in 14.6 inside'
WSH.echo(s.replace(/(\d+(\.\d*)?)\sin\b/g, inToCm));