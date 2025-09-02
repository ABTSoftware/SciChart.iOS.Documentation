//
//  AppDelegate.swift
//  Tutorial 09 - ChartModifier To ViewModel
//
//  Created by CN10 on 01/09/25.
//

import UIKit
import SciChart

@main
class AppDelegate: UIResponder, UIApplicationDelegate {

    internal var window: UIWindow?

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        // Override point for customization after application launch.
    
        // Provide your License Key:
        SCIChartSurface.setRuntimeLicenseKey("")
        
        window = UIWindow(frame: UIScreen.main.bounds)
        window?.rootViewController = ViewController()
        window?.makeKeyAndVisible()
        
        return true
    }
}

