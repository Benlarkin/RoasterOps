// src/services/ProductApiClient.ts
import axios, { AxiosInstance } from "axios";

export interface Product {
    id: number;
    name: string;
    type: "Green" | "Roasted";
    unit: "Bag" | "Lbs";
    stockLevel: number;
    createdAt: Date;
}

interface RawProduct {
    productId: number;
    name: string;
    type: number;
    unit: number;
    stockLevel: number;
    createdAt: string;
}

export class ProductApiClient {
    private apiClient: AxiosInstance;

    constructor(baseURL: string = "http://localhost:5295/api") {
        this.apiClient = axios.create({
            baseURL,
            headers: { "Content-Type": "application/json" }
        });
    }

    /** Fetches products and maps raw API fields into our UI model */
    public async getProducts(): Promise<Product[]> {
        const response = await this.apiClient.get<RawProduct[]>("/Products");
        return response.data.map((p) => ({
            id: p.productId,
            name: p.name,
            type: p.type === 0 ? "Green" : "Roasted",
            unit: p.unit === 0 ? "Bag" : "Lbs",
            stockLevel: p.stockLevel,
            createdAt: new Date(p.createdAt)
        }));
    }
}
