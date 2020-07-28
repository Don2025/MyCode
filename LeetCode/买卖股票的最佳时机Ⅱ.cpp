#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    int maxProfit(vector<int>& prices) {
        if(prices.empty()) return 0;
        int ans = 0;
        for(int i = 0; i < prices.size(); i++)
        {
            ans += max(0,prices[i]-prices[i-1]);
        }
        return ans;
    }
};

int main()    //本地测试
{
    Solution s;
    vector<int> nums;
    int a[] = {7,1,5,3,6,4};
    for(int i = 1; i < sizeof(a)/sizeof(a[0]); i++)
    {
        nums.push_back(a[i]);
    }
    int _ = s.maxProfit(nums);
    cout << _ << endl;
    return 0;
}