using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using project.Properties;

namespace project
{
   public class MyTabControl : TabControl
    {
        CefSharp.WinForms.ChromiumWebBrowser browser = null;

        #region Tab控件全局变量
        //绘制选项卡的尺寸
        int pgSize1 = 15;
        int pgSize2 = 5;

        //标签索引
        private int pgIndex = 1;

        //动态新建Tab选项卡标题
        private string newPageCaptain = "新标签页";
        #endregion

        /// <summary>
        /// 标签页索引
        /// </summary>
        public int PgIndex
        {
            get { return pgIndex; }
            set { pgIndex = value; }
        }

        /// <summary>
        /// 动态新建Tab选项卡标题
        /// </summary>
        public string NewPageCaptain
        {
            get { return newPageCaptain; }
            set { newPageCaptain = value; }
        }

        public MyTabControl()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.Padding = new System.Drawing.Point(pgSize1, pgSize2);
            this.DrawItem += new DrawItemEventHandler(MyTabControl_DrawItem);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(MyTabControl_MouseDown);
        }

        //初始化便签页添加按钮
        public void SetPageAddBtn()
        {
            //初始化便签页添加按钮
            if (this.TabPages["tabFirst"] == null)
            {
                this.TabPages.Add("tabFirst", "");
                this.PgIndex++;
            }
        }

        /// <summary>
        /// 添加新标签页
        /// </summary>
        public void PageAdd()
        {
            //新建Tab页
            TabPage pg = new TabPage(NewPageCaptain);
            //设置Tab页样式
            pg.BorderStyle = BorderStyle.None;
            //设置Tab页ID
            pg.Name = string.Format("pg{0}", this.PgIndex - 1);

            //将新建的Tab页绑定到Tab中

            TabPage pgFirst = (this.TabPages["tabFirst"]);
            this.TabPages.Remove(this.TabPages["tabFirst"]);
            browser = new CefSharp.WinForms.ChromiumWebBrowser("http://www.mimrc.com/");
            browser.Dock = DockStyle.Fill;
            pg.Controls.Add(browser);
            this.TabPages.Add(pg);
            this.TabPages.Add(pgFirst);

            //设置Tab当前标签页为最新添加的页面
            this.SelectedIndex = this.TabPages.Count - 2;

            //Page索引加1
            this.PgIndex++;
        }

        /// <summary>
        /// Tab选项卡重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //获取当前Tab选项卡的绘图区域
                Rectangle myTabRect = this.GetTabRect(e.Index);

                //判断该Tab页是否为添加按钮
                if (!String.IsNullOrEmpty(this.TabPages[e.Index].Text))
                {

                    //先添加TabPage属性      
                    e.Graphics.DrawString(this.TabPages[e.Index].Text
                    , this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);

                    //再画一个矩形框   
                    using (Pen p = new Pen(Color.Transparent))
                    {
                        myTabRect.Offset(myTabRect.Width - (pgSize1 + 3), 2);
                        myTabRect.Width = pgSize1;
                        myTabRect.Height = pgSize1;
                        e.Graphics.DrawRectangle(p, myTabRect);

                    }

                    //画Tab选项卡右上方关闭按钮   
                    using (Pen objpen = new Pen(Color.Black))
                    {
                        //获取绘图区域的开始坐标位置
                        Point p1 = new Point(myTabRect.X, myTabRect.Y);

                        //画关闭关闭按钮   
                        Bitmap bt = new Bitmap(Resources.btnClose);
                        e.Graphics.DrawImage(bt, p1);
                    }
                }
                else
                {

                    //先添加TabPage属性      
                    e.Graphics.DrawString(this.TabPages[e.Index].Text
                    , this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);

                    //再画一个矩形框   
                    using (Pen p = new Pen(Color.Transparent))
                    {
                        myTabRect.Offset(myTabRect.Width - (pgSize1 + 3), 2);
                        myTabRect.Width = pgSize1;
                        myTabRect.Height = pgSize1;
                        e.Graphics.DrawRectangle(p, myTabRect);

                    }

                    //画Tab选项卡新增按钮   
                    using (Pen objpen = new Pen(Color.Black))
                    {
                        //获取绘图区域的开始坐标位置
                        Point p1 = new Point(myTabRect.X - 25, myTabRect.Y);

                        //画Tab选项卡新增按钮  
                        Bitmap bt = new Bitmap(Resources.btnPageAdd);
                        e.Graphics.DrawImage(bt, p1);
                    }
                }
                //释放绘图资源
                e.Graphics.Dispose();
            }
            //控件出现异常时进行捕获
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ExpTab控件已崩溃", MessageBoxButtons.OK);
            }
        }

        private void MyTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            //判断该Tab页是否为添加按钮
            if (!String.IsNullOrEmpty(this.SelectedTab.Text))
            {
                if (e.Button == MouseButtons.Left)
                {
                    int x = e.X, y = e.Y;

                    //计算关闭区域      
                    Rectangle myTabRect = this.GetTabRect(this.SelectedIndex);

                    myTabRect.Offset(myTabRect.Width - (pgSize1 + 3), 2);
                    myTabRect.Width = pgSize1;
                    myTabRect.Height = pgSize1;

                    //如果鼠标在区域内就关闭选项卡      
                    bool isClose = x > myTabRect.X && x < myTabRect.Right
                     && y > myTabRect.Y && y < myTabRect.Bottom;

                    if (isClose == true)
                    {
                        this.TabPages.Remove(this.SelectedTab);
                    }
                }
            }
            else
            {
                this.PageAdd(); //当Tab页为添加按钮添加新标签页
            }
        }
    }
}
