#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    vector<vector<int> > permute(vector<int>& nums) {
        sort(nums.begin(),nums.end());
        vector<vector<int> > ans;
        do{
           ans.push_back(nums);
        }while(next_permutation(nums.begin(),nums.end()));
        return ans;
    }
};

int main()
{
    ios::sync_with_stdio(false);
    cin.tie(0),cout.tie(0);
    Solution s;
    vector<int> v;
    for(int i = 1; i < 4; i++)
    {
        v.push_back(i);
    }
    vector<vector<int> > ans;
    ans = s.permute(v);
    return 0;
}
