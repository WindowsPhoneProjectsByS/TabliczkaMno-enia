using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Button[] buttons = new Button[9];
        private Random random = new Random();
        List<int> randedValues = new List<int>();
        List<int> actualValues;
        int properPosition = 0;
        private DispatcherTimer timer;
        Button clickedButton;

        private int firstValue = 0;
        private int secondValue = 0;
        private int result = 0;
        private int loseCount = 0;
        private int winCount = 0;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            initAllButonns();
            addClickMethodsToAllButtons();
            initGame();

            timer = new DispatcherTimer();
            timer.Tick += initGameEvent;
            timer.Interval = new TimeSpan(0, 0, 2);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void initAllButonns()
        {
            buttons[0] = Button0;
            buttons[1] = Button1;
            buttons[2] = Button2;
            buttons[3] = Button3;
            buttons[4] = Button4;
            buttons[5] = Button5;
            buttons[6] = Button6;
            buttons[7] = Button7;
            buttons[8] = Button8;
        }

        private void addClickMethodsToAllButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Click += Button_Click;
            }    
        }

        private void initGameEvent(object sender, object e)
        {
            timer.Stop();
            addClickMethodsToAllButtons();
            restoreDefaultBackground();
            initGame();
        }

        private void initGame()
        {
            randNewValues();
            prepareCalculationAndRateTitle();
            prepareActualValues();
            prepareButtonsContent();
        }

        private void randNewValues()
        {
            firstValue = random.Next(1, 10);
            secondValue = random.Next(1, 10);
            result = firstValue * secondValue;
        }

        private void prepareCalculationAndRateTitle()
        {
            Calculation.Text = firstValue.ToString() + " * " + secondValue.ToString();
            RateTable.Text = winCount.ToString() + " : " + loseCount.ToString();
        }

        private void prepareActualValues()
        {
            int newValue = 0;
            properPosition = newValue = random.Next(0, 8);

            actualValues = null;
            actualValues = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                if (i != properPosition)
                {
                    do
                    {
                        newValue = random.Next(1, 10);
                        newValue = newValue * firstValue;

                    } while (actualValues.Contains(newValue) || newValue == result);

                    actualValues.Add(newValue);
                }
                else
                {
                    actualValues.Add(result);
                }

            }
        }

        private void prepareButtonsContent()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Content = actualValues[i];
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            clickedButton = button;
            checkResultClickChangeBackgrounds(button);
            removeClickMethodsToAllButtons();

            timer.Start();

        }

        private void checkResultClickChangeBackgrounds(Button button)
        {
            int resultNumber = Int32.Parse(button.Content.ToString());

            if (resultNumber == result)
            {
                winCount++;
                button.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                loseCount++;
                buttons[properPosition].Background = new SolidColorBrush(Colors.Green);
                button.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void removeClickMethodsToAllButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Click -= Button_Click;
            }
        }

        

        private void restoreDefaultBackground()
        {
            buttons[properPosition].Background = new SolidColorBrush(Colors.Black);
            clickedButton.Background = new SolidColorBrush(Colors.Black);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
