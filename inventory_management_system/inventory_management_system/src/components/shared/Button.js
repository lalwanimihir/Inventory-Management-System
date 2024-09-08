export default function Button({ onSort, columnName, sortOrder }) {
  const getSortIndicator = () => {
    if (sortOrder === "asc") return "↑";
    if (sortOrder === "desc") return "↓";
    return "";
  };
  return (
    <button className="btn btn-sm text-white fw-bold fs-6" onClick={onSort}>
      {columnName}
      {getSortIndicator()}
    </button>
  );
}
