using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        //There was a need to track what state the application was in to determine if a new number should be inputted
        //on the screen or if the number should be added to the existing number. An example of this is what happens when 
        //a user presses the equals button. The next time a number is pressed, it is expected that a new number is created.
        bool newNumber = false; 

        
        public Form1()
        {
            InitializeComponent();
        }
//All of the button click events are listed with this region. Most of these events simply call a custom method further down in the code.
#region Number Click Events

        private void btn1_Click(object sender, EventArgs e)
        {
            _numberClick("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            _numberClick("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            _numberClick("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            _numberClick("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            _numberClick("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            _numberClick("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            _numberClick("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            _numberClick("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            _numberClick("9");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            _numberClick("0");
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            //if there is the string "Cannot Divide" on the screen, it will clear it first.
            //this branching statement checks to see if there is a number already present before adding a period.
            _clearCannotDivide(); 
            if (newNumber == false) 
            {
                if (String.IsNullOrEmpty(lbl1.Text))
                {
                    lbl1.Text = lbl1.Text + "0.";
                }
                else
                {
                    lbl1.Text = lbl1.Text + ".";
                }
            }
            else //this else statement adds a "0." if a new number is required.
            {
                newNumber = false;
                lbl1.Text = "0.";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _clear(); //only clears the main label text.
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            _clearAll(); //clears out everything.
        }

        #endregion
//This region includes all the click events for the operators "/,*,-,+".
#region operation button click events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _operationClickEvent("+");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            _operationClickEvent("*");
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            _operationClickEvent("-");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            _operationClickEvent("/");
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            //again we clear the Cannot Divide text first if it is present.
            if (lbl1.Text == "Cannot Divide") 
            {
                _clearAll();
            }
            else
            {
                //we are making sure that all the needed information is present to perform the math.
                if (String.IsNullOrEmpty(var1.Text) && String.IsNullOrEmpty(var2.Text)) 
                {
                    //if it isn't present, nothing happens.
                }
                else
                {
                    //we have to what the last operator that was selected. this is done in var2 and var3.
                    if (var2.Text == var3.Text)
                    {
                        lbl1.Text = _equals(var2.Text);
                    }
                    else
                    {
                        lbl1.Text = _equals(var3.Text);
                    }
                    //we clear everything that isn't in the main label.
                    _clearMost(); 
                    newNumber = true; //the next time a number is pressed it will clear the main label first.
                }
            }
        }
#endregion

#region Custom_Methods

       private void _clear()
       {
           lbl1.Text = string.Empty;
           return;
       }

       private void _clearAll()
       {
           lbl1.Text = string.Empty;
           lbl2.Text = string.Empty;
           var1.Text = string.Empty;
           var2.Text = string.Empty;
           var3.Text = string.Empty;
           return;
       }
        private void _clearMost()
       {
           lbl2.Text = string.Empty;
           var1.Text = string.Empty;
           var2.Text = string.Empty;
           var3.Text = string.Empty;
       }

        string _equals(string currentOperator)
        {
            //we are explicitly converting the string text so we can do the math.
            //it might be better to do this at the click event instead. that would make this code more usable.
                decimal x = Convert.ToDecimal(var1.Text); 
                decimal y = Convert.ToDecimal(lbl1.Text); 
                decimal total;

                if (currentOperator == "+")
                {
                    total = x + y;
                    return Convert.ToString(total);
                }
                if (currentOperator == "-")
                {
                    total = x - y;
                    return Convert.ToString(total);
                }
                if (currentOperator == "*")
                {
                    total = x * y;
                    return Convert.ToString(total);
                }
                if (currentOperator == "/")
                {
                    //if you try to divide by "0" it will throw this "Cannot Divide" text. Later this text has to be cleared before anything else can be done.
                    if (y == 0) 
                    {
                        return "Cannot Divide";
                    }
                    else
                    {
                        total = x / y;
                        return Convert.ToString(total);
                    }
                }
                //this statement just completes the code block, it should never get to this point.
                return string.Empty; 
        }
        //first click means the first time a user clicks on an operator button.
        private void _firstClick(string firstClickOperator) 
        {
            //if no number is found in the main window the first time an operator is clicked, it will automatically add a "0".
            if (string.IsNullOrEmpty(lbl1.Text)) 
            {
                var1.Text = "0";
                var2.Text = firstClickOperator;
                var3.Text = var2.Text;
                lbl2.Text = var1.Text + " " + firstClickOperator;
                newNumber = true;
            }
            else
            {
                var1.Text = lbl1.Text;
                var2.Text = firstClickOperator;
                var3.Text = var2.Text;
                lbl2.Text = var1.Text + " " + firstClickOperator;
                newNumber = true;
            }
        }
        //naturally second click is the consecutive time the user clicks on the operator button.
        //there are added checks for the Cannont Divide text. It will also perform the equals function as if the
        //the equals button was pushed.
        private void _secondClick(string secondClickOperator) 
        {
           if (var2.Text == var3.Text)
            {
               lbl1.Text = _equals(var2.Text);
               if (lbl1.Text == "Cannot Divide")
               {
                   _clearMost();
               }
               else
               {
                   var1.Text = lbl1.Text;
                   lbl2.Text = var1.Text + " " + secondClickOperator;
                   var3.Text = secondClickOperator;
                   newNumber = true;
               }
            }
           else
           {
               if (lbl1.Text == "Cannot Divide")
               {
                   _clearMost();
               }
               else
               {
                   lbl1.Text = _equals(var3.Text);
                   var1.Text = lbl1.Text;
                   lbl2.Text = var1.Text + " " + secondClickOperator;
                   var3.Text = secondClickOperator;
                   newNumber = true;
               }
           }
        }
        //everytime a number is pushed it runs this code block. it checks for the Cannot Divide text,
        //sets the appropiate number text in the main window(label), and tracks whether the new number is needed.
        private void _numberClick(string number)
        {
            _clearCannotDivide();
            if (newNumber == false)
                lbl1.Text = lbl1.Text + number;
            else
                lbl1.Text = number;
            newNumber = false;
        }
        //the now infamous clearing of the Cannot Divide text
        private void _clearCannotDivide()
        {
            if (lbl1.Text == "Cannot Divide")
                lbl1.Text = string.Empty;
        }
        //the following code runs when an operator is clicked. It to clears the Cannot Divide text,
        //and then determines if it should run the first click or second click method.
        private void _operationClickEvent(string recentlyClickedOperator)
        {
            if (lbl1.Text == "Cannot Divide")
            {
                _clearAll();
            }
            else
            {
                if (String.IsNullOrEmpty(var2.Text))
                {

                    _firstClick(recentlyClickedOperator);
                }
                else
                {
                    _secondClick(recentlyClickedOperator);

                }
            }
        }

#endregion
//Standard tool strip items.
#region Tool Strip
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clear();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _clearAll();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox1 box = new AboutBox1())
            {
                box.ShowDialog(this);
            }

        }
#endregion

        

    }
}
