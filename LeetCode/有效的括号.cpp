#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    bool isValid(string s) {
        stack<char> st;
        for(auto it : s)
        {
            switch(it)
            {
                case '(': case '[': case '{': st.push(it); break;
                case ')':
                    if(st.empty() || st.top()!='(')
                    {
                        return false;
                    }
                    st.pop(); break;
                case ']':
                    if(st.empty() || st.top()!='[')
                    {
                        return false;
                    }
                    st.pop(); break;
                case '}':
                    if(st.empty() || st.top()!='{')
                    {
                        return false;
                    }
                    st.pop(); break;
                default : break;
            }
        }
        return st.empty();
    }
};

int main()
{
    Solution S;
    string s;
    cin >> s;
    cout << S.isValid(s) << endl;
    return 0;
}
