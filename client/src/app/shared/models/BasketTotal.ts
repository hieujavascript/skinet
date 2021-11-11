export interface IBasketTotal {
    shipCode: number;
    subTotal: number;
    total: number;
}
export class BasketTotal implements IBasketTotal {
    shipCode: number = 0;
    subTotal: number = 0;
    total: number = 0;

}