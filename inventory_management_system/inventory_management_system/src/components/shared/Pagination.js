import React from "react";

export default function Pagination({
  currentPage,
  totalPage,
  onPageChange,
  onPreviousPage,
  onNextPage,
}) {
  const getPageNumbers = () => {
    const pages = [];

    if (totalPage <= 4) {
      for (let i = 1; i <= totalPage; i++) {
        pages.push(i);
      }
    } else {
      if (currentPage <= 3) {
        pages.push(1, 2, 3, "...", totalPage);
      } else if (currentPage >= totalPage - 2) {
        pages.push(1, "...", totalPage - 2, totalPage - 1, totalPage);
      } else {
        pages.push(
          1,
          "...",
          currentPage - 1,
          currentPage,
          currentPage + 1,
          "...",
          totalPage
        );
      }
    }
    return pages;
  };

  const pageNumbers = getPageNumbers();

  return (
    <nav aria-label="Page navigation example">
      <ul className="pagination flex-wrap ml-3">
        <li className="page-item">
          <button
            className={`page-link ${currentPage === 1 ? "disabled" : ""}`}
            onClick={onPreviousPage}
            disabled={currentPage === 1}
          >
            Previous
          </button>
        </li>
        {pageNumbers.map((btn, index) => (
          <li key={index} className="page-item">
            {btn === "..." ? (
              <span className="page-link">...</span>
            ) : (
              <button
                onClick={() => onPageChange(btn)}
                className={`page-link ${currentPage === btn ? "active" : ""}`}
                style={{
                  backgroundColor: currentPage === btn ? "#386eb4" : "",
                  color: currentPage === btn ? "white" : "",
                }}
              >
                {btn}
              </button>
            )}
          </li>
        ))}
        <li className="page-item">
          <button
            className={`page-link ${
              currentPage === totalPage ? "disabled" : ""
            }`}
            onClick={onNextPage}
            disabled={currentPage === totalPage}
          >
            Next
          </button>
        </li>
      </ul>
    </nav>
  );
}
