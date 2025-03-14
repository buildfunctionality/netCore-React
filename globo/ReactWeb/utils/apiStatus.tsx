type Args = {
    status: "success" | "error" | "pending"
};

const ApiStatus = ({ status } : Args) => {
    switch (status) {
        case "error":
            return <div>Error communication with the data backend</div>;
           
        case "pending":
            return <div> Loading </div>;
        default:
            throw Error("Unknown API Status");
    }
}

export default ApiStatus;