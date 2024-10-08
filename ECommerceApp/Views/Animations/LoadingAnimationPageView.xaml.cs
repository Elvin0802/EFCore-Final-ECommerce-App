using ECommerceApp.Views.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ECommerceApp.Views.Animations;

public partial class LoadingAnimationPageView : Page
{
	public LoadingAnimationPageView()
	{
		InitializeComponent();

		// Start animations when the page is loaded
		Loaded += LoadingPage_Loaded;
	}
	private DispatcherTimer closeTimer;
	public Page Page { get; set; }

	private void LoadingPage_Loaded(object sender, RoutedEventArgs e)
	{
		// Create the rotation animation for the square (rotates around its center)
		DoubleAnimation rotateAnimation = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(3)));
		rotateAnimation.RepeatBehavior = RepeatBehavior.Forever;
		SquareRotate.BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, rotateAnimation);

		// Create move animations for the circles to orbit around the square
		StartCircleAnimation(Circle1Transform, 0, -70);    // Top
		StartCircleAnimation(Circle2Transform, 70, 0);     // Right
		StartCircleAnimation(Circle3Transform, 0, 70);     // Bottom
		StartCircleAnimation(Circle4Transform, -70, 0);    // Left
		StartCircleAnimation(Circle5Transform, 60, -60);   // Top-right diagonal
		StartCircleAnimation(Circle6Transform, -60, 60);   // Bottom-left diagonal

		// Start a timer to close the animation page after a delay
		closeTimer = new DispatcherTimer();
		closeTimer.Interval = TimeSpan.FromMilliseconds(1500); // Set the delay to 5 seconds (you can change it)
		closeTimer.Tick += CloseAnimationPage;
		closeTimer.Start();
	}

	private void StartCircleAnimation(TranslateTransform circleTransform, double xMove, double yMove)
	{
		// Create the X movement animation
		DoubleAnimationUsingKeyFrames xAnimation = new DoubleAnimationUsingKeyFrames();
		xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(xMove, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(700))));
		xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1400))));
		xAnimation.RepeatBehavior = RepeatBehavior.Forever;
		circleTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);

		// Create the Y movement animation
		DoubleAnimationUsingKeyFrames yAnimation = new DoubleAnimationUsingKeyFrames();
		yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(yMove, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(700))));
		yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1400))));
		yAnimation.RepeatBehavior = RepeatBehavior.Forever;
		circleTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
	}

	private void CloseAnimationPage(object sender, EventArgs e)
	{
		closeTimer.Stop();  // Stop the timer

		// Navigate to another page (replace with the page you want to navigate to)
		App.Container.GetInstance<MainWindowView>().MainContentFrame.Navigate(Page);
	}
}
