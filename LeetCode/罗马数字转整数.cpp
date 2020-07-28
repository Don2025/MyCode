#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    int romanToInt(string s) {
        int len = s.length();
        int sum = 0;
        for(int i = 0; i < len; i++) {
            if(i == len-1) {
                sum += getNum(s[i]);
                return sum;
            }
            if(getNum(s[i]) < getNum(s[i+1])) {
                sum -= getNum(s[i]);
            }
            else sum += getNum(s[i]);
        }
    }
    int getNum(char c) {
        int num;
        switch(c) {
            case 'I': num = 1; break;
            case 'V': num = 5; break;    
            case 'X': num = 10; break;    
            case 'L': num = 50; break;    
            case 'C': num = 100; break;                
            case 'D': num = 500; break;    
            case 'M': num = 1000; break;                
            default: break;
        }
        return num;
    }
};

int main()
{
    Solution s;
    string str;
    cin >> str;
    cout << s.romanToInt(str) << endl;
    return 0;
}