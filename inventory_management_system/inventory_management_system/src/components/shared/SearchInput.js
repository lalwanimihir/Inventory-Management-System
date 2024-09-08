export default function SearchInput({ onSearchChange }) {
  return (
    <div className="row py-3 rounded-3 ">
      <div className="col-md-6 m-auto d-flex">
        <label htmlFor="search" className="form-label fw-bold"></label>
        <input
          id="search"
          type="text"
          name="search"
          onChange={onSearchChange}
          placeholder="Search..."
          className="form-control border"
          style={{ backgroundColor: "#d7e0eb" }}
        />
        <button
          className="btn"
          style={{
            backgroundColor: "#386eb4",
            color: "white",
            cursor: "default",
          }}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="white"
            className="bi bi-search"
            viewBox="0 0 16 16"
          >
            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
          </svg>
        </button>
      </div>
    </div>
  );
}
