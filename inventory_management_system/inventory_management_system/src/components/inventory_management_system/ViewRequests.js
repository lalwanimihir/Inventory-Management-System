import Modal from "../shared/Modal";
import { useGetAllInventoryRequestsQuery } from "../../services/inventory_management/ProductDetailsApiSlice";
import InventoryRequestsInfo from "./InventoryRequestsInfo";
import NoData from "../shared/NoData";
import Loading from "../shared/Loading";
import { useState } from "react";
import Pagination from "../shared/Pagination";

export default function ViewRequests() {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 5;
  const {
    data = {},
    error,
    isLoading,
  } = useGetAllInventoryRequestsQuery({
    pageNo: currentPage,
    pageSize,
  });

  const { totalCount = 0, inventoryRequests = [] } = data;

  const totalPage = Math.ceil(totalCount / pageSize);
  const totalPageArray = new Array(totalPage);
  totalPageArray.fill("0");

  const handleNextPage = () => {
    if (currentPage < totalPage) {
      setCurrentPage((prevPage) => {
        const newPage = prevPage + 1;
        return newPage;
      });
    }
  };
  const handlePreviousPage = () => {
    setCurrentPage((prevPage) => Math.max(prevPage - 1, 1));
  };

  function findCurrentPage(pageNo) {
    setCurrentPage(pageNo);
  }

  if (isLoading) return <Loading />;
  if (error) {
    return <NoData message="Error loading data!" />;
  }

  return (
    <Modal classes="modal-lg">
      <div className="modal-body">
        <table className="table table-striped tex-center">
          <thead className="table-dark">
            <tr>
              <th className="pt-3 ">Sr. No</th>
              <th className="pt-3 ">Product Name</th>
              <th className="pt-3 ">Quantity</th>
              <th className="pt-3 ">Requested By</th>
              <th className="pt-3 ">Status</th>
              <th className="pt-3 ">Action</th>
            </tr>
          </thead>
          <tbody>
            {inventoryRequests && inventoryRequests.length > 0 ? (
              inventoryRequests.map((values, index) => (
                <InventoryRequestsInfo
                  key={values.id}
                  index={index}
                  userName={values.applicationUser.userName}
                  userId={values.applicationUser.userId}
                  {...values}
                  currentPage={currentPage}
                  pageSize={pageSize}
                />
              ))
            ) : (
              <NoData message="Data not available!" />
            )}
          </tbody>
        </table>
        <Pagination
          currentPage={currentPage}
          totalPage={totalPage}
          onPageChange={findCurrentPage}
          onPreviousPage={handlePreviousPage}
          onNextPage={handleNextPage}
        />
      </div>
    </Modal>
  );
}
