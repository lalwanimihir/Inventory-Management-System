const statusColors = {
  1: "#e26c6c",
  2: "#78d85c",
  3: "#d5c43c",
};

const progressStatus = ["", "Rejected", "Approved", "On Hold", "In Process"];

function getStatusColor(status) {
  return statusColors[status] || "";
}

export default function UserInventoryRequestsInfo({
  id,
  index,
  productName,
  quantity,
  reason,
  status,
  currentPage,
  pageSize,
}) {
  const backgroundColor = getStatusColor(status);
  return (
    <tr key={index}>
      <td style={{ backgroundColor }}>
        {(currentPage - 1) * pageSize + index + 1}
      </td>
      <td style={{ backgroundColor }}>{productName}</td>
      <td style={{ backgroundColor }}>{quantity}</td>
      <td style={{ backgroundColor }}>{reason}</td>
      <td style={{ backgroundColor }}>{progressStatus[status]}</td>
    </tr>
  );
}
