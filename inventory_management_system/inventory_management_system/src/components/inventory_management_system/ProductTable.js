import React from "react";
import Button from "../shared/Button";
import ProductsInfo from "../../components/inventory_management_system/ProductsInfo";
import NoData from "../shared/NoData";

export default function ProductTable({
  products,
  onDelete,
  onUpdate,
  updateActivity,
  isActive,
  onSort,
  sortBy,
  sortOrder,
  currentPage,
  pageSize,
}) {
  return (
    <div className="table-responsive ">
      <table className="table shadow  text-center">
        <thead>
          <tr>
            <th>
              <Button onSort={() => onSort("Sr No")} columnName="Sr No" />
            </th>
            <th>
              <Button
                onSort={() => onSort("productName")}
                columnName="Product Name"
                sortOrder={
                  sortBy === "productName" ? (sortOrder ? "asc" : "desc") : null
                }
              />
            </th>
            <th>
              <Button
                onSort={() => onSort("category")}
                columnName="Category"
                sortOrder={
                  sortBy === "category" ? (sortOrder ? "asc" : "desc") : null
                }
              />
            </th>
            <th>
              <Button
                onSort={() => onSort("quantity")}
                columnName="Quantity"
                sortOrder={
                  sortBy === "quantity" ? (sortOrder ? "asc" : "desc") : null
                }
              />
            </th>
            <th>
              <Button
                onSort={() => onSort("price")}
                columnName="Price"
                sortOrder={
                  sortBy === "price" ? (sortOrder ? "asc" : "desc") : null
                }
              />
            </th>
            <th className="text-white">Action</th>
          </tr>
        </thead>
        <tbody>
          {products && products.length > 0 ? (
            products?.map((values, index) => (
              <ProductsInfo
                key={values.id}
                index={index}
                onDelete={onDelete}
                onUpdate={onUpdate}
                updateActivity={updateActivity}
                isActive={isActive}
                currentPage={currentPage}
                pageSize={pageSize}
                {...values}
              />
            ))
          ) : (
            <NoData message="Data is not available" />
          )}
        </tbody>
      </table>
    </div>
  );
}
