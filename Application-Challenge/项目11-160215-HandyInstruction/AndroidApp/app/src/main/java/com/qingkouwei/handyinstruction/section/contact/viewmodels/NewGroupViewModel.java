package com.qingkouwei.handyinstruction.section.contact.viewmodels;

import android.app.Application;
import androidx.annotation.NonNull;
import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupOptions;
import com.qingkouwei.handyinstruction.common.livedatas.SingleSourceLiveData;
import com.qingkouwei.handyinstruction.common.net.Resource;
import com.qingkouwei.handyinstruction.common.repositories.EMGroupManagerRepository;

public class NewGroupViewModel extends AndroidViewModel {
    private EMGroupManagerRepository repository;
    private SingleSourceLiveData<Resource<EMGroup>> groupObservable;

    public NewGroupViewModel(@NonNull Application application) {
        super(application);
        repository = new EMGroupManagerRepository();
        groupObservable = new SingleSourceLiveData<>();
    }

    public LiveData<Resource<EMGroup>> groupObservable() {
        return groupObservable;
    }

    public void createGroup(String groupName, String desc, String[] allMembers, String reason, EMGroupOptions option) {
        groupObservable.setSource(repository.createGroup(groupName, desc, allMembers, reason, option));
    }
}
