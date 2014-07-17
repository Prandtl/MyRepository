__author__ = 'Vasiliy'
print("input string:",end=' ')
str=input().split(" ")
maxIndex=0
maxLength=0
for i in range(len(str)):
    if(len(str[i])>maxLength):
        maxLength=len(str[i])
        maxIndex=i
print("the longest word is: ", str[maxIndex])