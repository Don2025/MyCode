#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    int maxArea(vector<int>& height) {
        int sz = height.size();
        if(sz < 1) return -1;
        int ans = 0;
        for(int i = 0, j = sz-1; i < j; )
        {
            int h = min(height[i],height[j]);  //取最短板
            ans = max(ans,h*(j-i));
            if(height[i] < height[j]) i++;
            else j--;
        }
        return ans;
    }
};

int main()
{
    ios::sync_with_stdio(false);
    cin.tie(0),cout.tie(0);
    Solution s;
    int a[9] = {1,8,6,2,5,4,8,3,7};
    vector<int> v;
    for(int i = 0; i < 9; i++)
    {
        v.push_back(a[i]);
    }
    cout << s.maxArea(v) << endl;
    return 0;
}
