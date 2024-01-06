import { makeAutoObservable } from "mobx";
import { Modal } from "../models/Logical/modal";

class ModalStore {
    modal: Modal = {
        open: false,
        body: null
    }

    constructor() {
        makeAutoObservable(this)
    }

    openModal = (content: JSX.Element) => {
        this.modal.open = true;
        this.modal.body = content;
    }

    closeModal = () => {
        this.modal.open = false;
        this.modal.body = null;
    }
}

export default ModalStore;