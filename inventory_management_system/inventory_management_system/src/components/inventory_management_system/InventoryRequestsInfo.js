import { useState } from "react";
import { useUpdateInventoryRequestStatusMutation } from "../../services/inventory_management/ProductDetailsApiSlice";

const statusColors = {
  1: "red",
  2: "green",
  3: "#f7a000",
};

const progressStatus = ["", "Rejected", "Approved", "On Hold", "In Process"];

function getStatusColor(status) {
  return statusColors[status] || "";
}

export default function InventoryRequestsInfo({
  id,
  index,
  productName,
  quantity,
  status,
  userName,
  currentPage,
  pageSize,
}) {
  const [statusValue, setStatus] = useState("");
  const [updateStatus] = useUpdateInventoryRequestStatusMutation();
  const updateRequestStatus = async (status, id) => {
    await updateStatus({ id, status: +status });
  };
  const handleSelectChange = (event, id) => {
    const selectedStatus = event.target.value;
    setStatus(selectedStatus);
    updateRequestStatus(selectedStatus, id);
  };
  const color = getStatusColor(status);
  return (
    <tr key={index}>
      <td>{(currentPage - 1) * pageSize + index + 1}</td>
      <td>{productName}</td>
      <td>{quantity}</td>
      <td>{userName}</td>
      <td style={{ color }}>{progressStatus[status]}</td>
      <td>
        <select
          className="form-select"
          aria-label="Default select example"
          value={statusValue}
          onChange={(e) => handleSelectChange(e, id)}
        >
          <option value="" disabled hidden>
            Apply Status
          </option>
          <option value="1">Reject</option>
          <option value="2">Approve</option>
          <option value="3">On Hold</option>
        </select>
      </td>
    </tr>
  );
}
