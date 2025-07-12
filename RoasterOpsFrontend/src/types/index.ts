export type ProductType = 'Green' | 'Roasted'
export type UnitType = 'Bag' | 'Lbs'
export type OrderStatus = 'Pending' | 'Fulfilled'

export interface Product {
    productId: number
    name: string
    type: ProductType
    unit: UnitType
    stockLevel: number
    createdAt: string
}

export interface OrderLineItem {
    orderLineItemId: number
    orderId: number
    productId: number
    quantity: number
    product?: Product
}

export interface Order {
    orderId: number
    customerName: string
    orderDate: string
    status: OrderStatus
    lineItems: OrderLineItem[]
}
