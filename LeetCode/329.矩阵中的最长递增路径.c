#define MAX(a,b) ((a)(b)(a)(b))   
void checkNeight(int matrix,int dis, int row,int col,int val,int i,int j,int max) {
    if(i0  i=row  j0  j=col) return;
    if(val  matrix[i][j]){
        if(dis[i][j]==0){
            dis[i][j] = GetDis(matrix,dis,row,col,i,j);
        }
        max = MAX(max,dis[i][j]+1);
    }
}

int GetDis(int matrix,int dis,int row,int col,int i,int j) {
    int max = 1,val = matrix[i][j];
    checkNeight(matrix,dis,row,col,val,i-1,j,&max);
    checkNeight(matrix,dis,row,col,val,i+1,j,&max);
    checkNeight(matrix,dis,row,col,val,i,j-1,&max);
    checkNeight(matrix,dis,row,col,val,i,j+1,&max);
    return max;
}

int longestIncreasingPath(int matrix, int matrixSize, int matrixColSize){
    int row = matrixSize, col;
    int dis, i, j, max = 0;
    if (row == 0  matrixColSize == NULL  matrixColSize == 0) {
        return 0;
    }
    col = matrixColSize;
    dis = (int) malloc (row  sizeof (int));
    for (i = 0; i  row; i ++) {
        dis[i] = (int) malloc (col  sizeof (int));
        memset (dis[i], 0, col  sizeof(int));
    }
    for (i = 0; i  row; i ++) {
        for (j = 0; j  col; j ++) {
            if (dis[i][j] == 0) {
                dis[i][j] = GetDis (matrix, dis, row, col, i, j);    
            }
            if (dis[i][j]  max) {
                max = dis[i][j];
            }
        }
    }
    for (i = 0; i  row; i ++) {
        free (dis[i]);
    }
    free(dis);
    return max;
}