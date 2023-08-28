import React, { useEffect, useState } from 'react';
import { getCategories, getAllProducts, addOrUpdateProduct, deleteProduct } from './api'; 

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const [categories, setCategories] = useState([]);
  
    useEffect(() => {
      fetchProducts();
      fetchCategories();
    }, []);
  
    const fetchProducts = async () => {
      try {
        const productsData = await getAllProducts();
        setProducts(productsData);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };
  
    const fetchCategories = async () => {
      try {
        const categoriesData = await getCategories();
        setCategories(categoriesData);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };
  
    const handleProductUpdate = async (product) => {
      try {
        const updatedProduct = await addOrUpdateProduct(product);
        // Handle the updated product data if needed
      } catch (error) {
        console.error('Error updating product:', error);
      }
    };
  
    const handleProductDelete = async (productId) => {
      try {
        await deleteProduct(productId);
        // Update the products list after deletion
        fetchProducts();
      } catch (error) {
        console.error('Error deleting product:', error);
      }
    };
  return (
    <div>
      <h2>Product List</h2>
      <ul>
        {products.map(product => (
          <li key={product.id}>{product.name}</li>
        ))}
      </ul>
    </div>
  );
}

export default ProductList;
