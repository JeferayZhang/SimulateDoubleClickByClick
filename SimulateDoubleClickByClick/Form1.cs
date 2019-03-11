using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimulateDoubleClickByClick
{
    public partial class FrmTest : Form
    {
        // 跟踪双击事件触发的次数
        int count = 0;
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            DoubleClickButton dClickB = new DoubleClickButton();

            dClickB.Bounds = new Rectangle(10, 10, 200, 30);
            dClickB.Text = "Double-click me!";
            Controls.Add(dClickB);

            // 给按钮附加双击事件
            dClickB.DoubleClick += new EventHandler(DClick);
        }
        private void DClick(object o, EventArgs e)
        {
            // 展示双击事件触发的次数
            MessageBox.Show("Double-click count = " + ++count);
        }
    }

    /// <summary>
    /// 创建一个按钮类
    /// </summary>
    public class DoubleClickButton : System.Windows.Forms.Button
    {
        // 双击时,第一次点击和第二次点击之前所间隔的最大毫秒数
        int previousClick = SystemInformation.DoubleClickTime;
        //双击事件委托
        public new event EventHandler DoubleClick;

        /// <summary>
        /// 重写父类单击方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            //获取启动系统后经过的毫秒数
            int now = System.Environment.TickCount;

            //判断事件间隔是否小于应有间隔,因为第一次点击时,结果肯定是false,那么previousClick实际上会在下面赋值,双击时,第二次点击才有可能进入
            if (now - previousClick <= SystemInformation.DoubleClickTime)
            {
                // Raise the DoubleClick event.
                if (DoubleClick != null)
                    DoubleClick(this, EventArgs.Empty);
            }
            //上次点击时间
            previousClick = now;

            // 允许调用父类的方法
            base.OnClick(e);
        }

        // 如果写了双击事件,那么也将调用上面定义的事件委托
        protected new virtual void OnDoubleClick(EventArgs e)
        {
            if (this.DoubleClick != null)
                this.DoubleClick(this, e);
        }
    }
}

