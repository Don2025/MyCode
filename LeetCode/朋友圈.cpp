class Solution {
public:
    bool vis[1005] = {false};
    int findCircleNum(vector<vector<int> > &M) 
    {
        int sz = M.size();
        int cnt = 0;
        for(int i = 0; i < sz; i++)
        {
            if(!vis[i])
            {
                dfs(M,i);
                cnt++;
            }
        }
        return cnt;
    }
    void dfs(vector<vector<int> > &M, int i) 
    {
        for(int j = 0; j < M.size(); j++)
        {
            if(M[i][j] && !vis[j])
            {
                vis[j] = true;
                dfs(M,j);
            }
        }
    }
};