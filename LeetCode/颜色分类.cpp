#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    void sortColors(vector<int>& nums) {
        int a[3] = {0};    //3¸öÍ°
        for(auto it : nums)
        {
            int i = it%3;
            a[i]++;
        }
        int cnt = 0;
        for(int i = 0; i < 3; i++)
        {
            int _ = a[i];
            for(int j = 0; j < a[i]; j++)
            {
                nums[cnt++] = i;
            }
        }
    }
};

int main()
{
    Solution s;
    vector<int> v={2,0,2,1,1,0};
    s.sortColors(v);
    for(auto it : v)
    {
        cout << it << " ";
    }
    return 0;
}
