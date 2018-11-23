﻿using Azylee.WinformSkin.APIUtils;
using Azylee.WinformSkin.FormUI.NoTitle;
using Azylee.WinformSkin.FormUI.Toast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Azylee.WinformSkin.FormUI.CustomTitle
{
    public partial class BigIconForm : NoTitleForm
    {
        public BigIconForm()
        {
            InitializeComponent();
        }

        private void BigIconForm_Load(object sender, EventArgs e)
        {
        }

        #region 属性
        private int _BigIconFormHeadHeight = 68;
        [Category("Style")]
        [Description("标题栏高度")]
        [DefaultValue(typeof(int), "68")]
        public int BigIconFormHeadHeight
        {
            get { return _BigIconFormHeadHeight; }
            set
            {
                if (_BigIconFormHeadHeight != value)
                {
                    _BigIconFormHeadHeight = value;
                    BigIconFormPNHead.Height = value;
                }
            }
        }
        private bool _DoubleClickMax = true;
        [Category("Style")]
        [Description("双击最大化窗口")]
        [DefaultValue(typeof(bool), "true")]
        public bool DoubleClickMax
        {
            get { return _DoubleClickMax; }
            set
            {
                if (_DoubleClickMax != value)
                {
                    _DoubleClickMax = value;
                }
            }
        }
        #endregion
        #region UI界面调整方法
        public void UIMax()
        {
            Invoke(new Action(() =>
            {
                if (WindowState != FormWindowState.Maximized)
                {
                    MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    WindowState = FormWindowState.Maximized;
                }
            }));
        }
        #endregion
        #region 窗口操作：拖动、边框、最小化、最大化、还原、双击标题栏最大化、拖动标题栏还原、关闭
        /// <summary>
        /// 拖动窗口移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BigIconFormLBHeadTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormStyleAPI.ReleaseCapture();
                FormStyleAPI.SendMessage(Handle, FormStyleAPI.WM_NCLBUTTONDOWN, FormStyleAPI.HTCAPTION, 0);
            }
        }
        /// <summary>
        /// 大小改变，刷新边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BigIconFormBigIconForm_SizeChanged(object sender, EventArgs e)
        {
            SetBorder();
            BigIconFormPNHead.Height = BigIconFormHeadHeight;
            TMRefresh.Enabled = true;

            if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal)
            {
                if (Visible && Opacity > 0)
                {
                    TMRefreshStart();
                    //ToastForm.Display("test", $"窗口显示，且为正常大小状态，透明度{Opacity}", 'i', 5000);
                }
            }
        }
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BigIconFormBTFormMinBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 最大化及还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BigIconFormBTFormMaxBox_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                WindowState = FormWindowState.Maximized;
            }
            else
                WindowState = FormWindowState.Normal;
        }
        private void BigIconFormLBHeadTitle_DoubleClick(object sender, EventArgs e)
        {
            if (_DoubleClickMax)
            {
                if (WindowState != FormWindowState.Maximized)
                {
                    MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    WindowState = FormWindowState.Maximized;
                }
                else
                    WindowState = FormWindowState.Normal;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BigIconFormBTFormCloseBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        #region 窗口显示优化
        /// <summary>
        /// 重绘窗口计时器，防止win10出现部分区域透明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TMRefreshStart()
        {
            TMRefresh.Interval = 200;
            TMRefresh.Enabled = true;
        }
        private void TMRefresh_Tick(object sender, EventArgs e)
        {
            int maxInterval = 200 + 6;
            if (TMRefresh.Interval > maxInterval)
            {
                TMRefresh.Enabled = false;
            }
            else
            {
                Refresh();
                TMRefresh.Interval = TMRefresh.Interval + 1;
            }
        }
        #endregion
    }
}
