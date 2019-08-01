//
//  ALEventService.h
//  AppLovinSDK
//
//  Created by Thomas So on 2/13/19
//  Copyright © 2019 AppLovin Corporation. All rights reserved.
//

#import "ALEventTypes.h"

NS_ASSUME_NONNULL_BEGIN

/**
 * Service used for tracking various analytical events.
 */
@interface ALEventService : NSObject

/**
 * Set a super property to be recorded with all future events.
 *
 * If the property is set to nil, will remove that super property from being recorded with all future events.
 *
 * @param superProperty The super property value for the given super property key.
 *                      Valid types include `NSString`, `NSNumber`, `NSSDate`, `NSURL`, `NSArray`, `NSDictionary`.
 *                      Setting it to nil will remove that super property from being recorded with all future events.
 * @param key           The super property key for the given super property.
 */
- (void)setSuperProperty:(nullable id)superProperty forKey:(NSString *)key;

/**
 * NSDictionary representing the currently set super properties that are passed up on events.
 */
@property (nonatomic, copy, readonly) NSDictionary<NSString *, id> *superProperties;

/**
 * Track an event without additional data.
 *
 * Where applicable, it is suggested to use one of the predefined strings provided in ALEventTypes.h for the event and parameter key.
 *
 * @param eventName A string representing the event to track.
 */
- (void)trackEvent:(NSString *)eventName;

/**
 * Track an event with additional data.
 *
 * Where applicable, it is suggested to use one of the predefined strings provided in ALEventTypes.h for the event and parameter key.
 *
 * @param eventName  A string representing the event to track.
 * @param parameters A dictionary containing key-value pairs further describing this event.
 */
- (void)trackEvent:(NSString *)eventName parameters:(nullable NSDictionary<NSString *, id> *)parameters;

/**
 * Track an in app purchase.
 *
 * Where applicable, it is suggested to use the pre-defined parameter keys provided in ALEventTypes.h. At a minimum, you should provide the following parameters: kALEventParameterProductIdentifierKey,
 * kALEventParameterRevenueAmountKey, and kALEventParameterRevenueCurrencyKey. If you pass a value for kALEventParameterStoreKitReceiptKey, it will be used for validation. Otherwise, we will automatically collect
 * [[NSBundle mainBundle] appStoreReceiptURL] and use it for validation.
 *
 * @param transactionIdentifier Value of -[SKTransaction transactionIdentifier] property.
 * @param parameters            A dictionary containing key-value pairs further describing this event.
 */
- (void)trackInAppPurchaseWithTransactionIdentifier:(NSString *)transactionIdentifier parameters:(nullable NSDictionary<NSString *, id> *)parameters;

/**
 * Track a checkout / standard purchase.
 *
 * Where applicable, it is suggested to use the pre-defined parameter keys provided in ALEventTypes.h. At a minimum, you should provide the following parameters: kALEventParameterProductIdentifierKey,
 * kALEventParameterRevenueAmountKey, and kALEventParameterRevenueCurrencyKey.
 *
 * @param transactionIdentifier An optional unique identifier for this transaction, as generated by you. For Apple Pay transactions, we suggest -[PKPaymentToken transactionIdentifier] property.
 * @param parameters            A dictionary containing key-value pairs further describing this event.
 */
- (void)trackCheckoutWithTransactionIdentifier:(nullable NSString *)transactionIdentifier parameters:(nullable NSDictionary<NSString *, id> *)parameters;


- (instancetype)init __attribute__((unavailable("Access ALEventService through ALSdk's eventService property.")));

@end

NS_ASSUME_NONNULL_END